using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Entities.Messages;
using DayZTradeCenter.DomainModel.Interfaces;
using Microsoft.AspNet.Identity;
using rg.GenericRepository.Core;
using rg.Time;

namespace DayZTradeCenter.DomainModel.Services
{
    public class TradeManager : ITradeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeManager"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <param name="itemsRepository">The items repository.</param>
        /// <param name="userStore">The user store.</param>
        /// <exception cref="ArgumentNullException">
        /// tradesRepository
        /// or
        /// itemsRepository
        /// or
        /// userStore
        /// </exception>
        public TradeManager(
            IRepository<Trade> tradesRepository, 
            IRepository<Item> itemsRepository, 
            IUserStore<ApplicationUser> userStore)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            if (itemsRepository == null)
            {
                throw new ArgumentNullException("itemsRepository");
            }

            if (userStore == null)
            {
                throw new ArgumentNullException("userStore");
            }

            _tradesRepository = tradesRepository;
            _itemsRepository = itemsRepository;
            _userStore = userStore;
        }

        #region Query API

        private IQueryable<Trade> All
        {
            get { return _tradesRepository.GetAll(); }
        }

        /// <summary>
        /// Gets the latest active trades ordered by creation date.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns>
        /// The latest active trades listed in reverse chronological order.
        /// </returns>
        public IEnumerable<Trade> GetLatestTrades(int count = 12)
        {
            return GetActiveTrades()
                .OrderByDescending(trade => trade.CreationDate)
                .Take(count);
        }

        /// <summary>
        /// Gets the hottest active trades ordered by number of offers.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns>
        /// The active trades listed by number of offers in descending order.
        /// </returns>
        public IEnumerable<Trade> GetHottestTrades(int count = 12)
        {
            return GetActiveTrades()
                .OrderByDescending(trade => trade.Offers.Count)
                .Take(count);
        }

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Trade> GetActiveTrades()
        {
            return All.Where(t => t.State == TradeStates.Active);
        }

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        /// <param name="params">The search parameters.</param>
        /// <returns>
        /// The active trades that satisfy the search parameters.
        /// </returns>
        /// <exception cref="System.NotSupportedException">
        /// The specified search type is not supported yet.
        /// </exception>
        public IEnumerable<Trade> GetActiveTrades(SearchParams @params)
        {
            var result = GetActiveTrades();

            if (@params.HardcoreOnly)
            {
                result = result.Where(t => t.IsHardcore);
            }
            else if (@params.ExpOnly)
            {
                result = result.Where(t => t.IsExperimental);
            }
            
            if (@params.ItemId.HasValue)
            {
                // this solves: "There is already an open DataReader associated with this Command which must be closed first."
                // NB: another viable solution is to turn on MultipleActiveResultSets or to disable lazy-loading and use "Include"
                // http://stackoverflow.com/questions/4867602/entity-framework-there-is-already-an-open-datareader-associated-with-this-comma
                var trades = 
                    result.ToArray(); // <-- the problem here is that *ALL* the trades are loaded into memory!

                switch (@params.Type)
                {
                    case SearchTypes.Have:
                        result = trades
                            .Where(t => t.Have.Any(i => i.Item.Id == @params.ItemId));
                        break;

                    case SearchTypes.Want:
                        result = trades
                            .Where(t => t.Want.Any(i => i.Item.Id == @params.ItemId));
                        break;

                    case SearchTypes.Both:
                        result = trades
                            .Where(t =>
                                t.Have.Any(i => i.Item.Id == @params.ItemId) ||
                                t.Want.Any(i => i.Item.Id == @params.ItemId));
                        break;

                    default:
                        throw new NotSupportedException("The specified search type is not supported yet.");
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the trades owned by the user with the specified user id.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Trade> GetTradesByUser(string userId)
        {
            return All.Where(t => t.Owner.Id == userId);
        }

        /// <summary>
        /// Gets the trades which the user with the specified id has offered to.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<Trade> GetOffersByUser(string userId)
        {
            return All.Where(t => t.Offers.Any(o => o.Id == userId));
        }

        /// <summary>
        /// Gets the trade by identifier.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <returns>
        /// The trade with the specified id.
        /// </returns>
        public Trade GetTradeById(int tradeId)
        {
            return _tradesRepository.GetSingle(tradeId);
        }

        /// <summary>
        /// Gets all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetAllItems()
        {
            return _itemsRepository.GetAll();
        }

        /// <summary>
        /// Determines whether the user with the specified use id can create a new trade.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the user can create a new trade, <c>false</c> otherwise.
        /// </returns>
        public bool CanCreateTrade(string userId)
        {
            // the active trades of the specified user. 
            var activeTrades =
                GetTradesByUser(userId)
                    .Where(trade => trade.State == TradeStates.Active);

            return activeTrades.Count() < 3;
        }

        #endregion

        /// <summary>
        /// Creates a new trade.
        /// </summary>
        /// <param name="have">The items for the "Have" section.</param>
        /// <param name="want">The items for the "Want" section.</param>
        /// <param name="isHardcore">if set to <c>true</c> the trade is for the hardcore public hive.</param>
        /// <param name="isExperimental">if set to <c>true</c> the trade is for the experimental public hive.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>True</c> if the trade was successfully created, <c>false</c> otherwise.
        /// </returns>
        public bool CreateNewTrade(
            IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, 
            bool isHardcore, bool isExperimental, ApplicationUser user)
        {
            if (!CanCreateTrade(user.Id))
            {
                return false;
            }

            var trade = new Trade {IsHardcore = isHardcore, IsExperimental = isExperimental};

            foreach (var itemDetails in have)
            {
                trade.Have.Add(
                    new TradeDetails(_itemsRepository.GetSingle(itemDetails.Id), itemDetails.Quantity));
            }
            foreach (var itemDetails in want)
            {
                trade.Want.Add(
                    new TradeDetails(_itemsRepository.GetSingle(itemDetails.Id), itemDetails.Quantity));
            }

            trade.Owner = user;
            
            trade.CreationDate = TimeProvider.Now;

            _tradesRepository.Insert(trade);
            _tradesRepository.SaveChanges();

            return true;
        }

        /// <summary>
        /// Deletes a trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the trade was successfully deleted, <c>false</c> otherwise.
        /// </returns>
        public bool DeleteTrade(int tradeId, string userId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            if (trade.Owner.Id == userId)
            {
                SendMessage(trade.Offers, () => new TradeDeletedMessage());

                // TODO: fix delete with proper "on delete cascade". This is just a patch!
                trade.Want.ToList().ForEach(d => trade.Want.Remove(d));
                trade.Have.ToList().ForEach(d => trade.Have.Remove(d));

                _tradesRepository.Delete(trade);
                _tradesRepository.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds an offer for the specified trade from the given user.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <see cref="OfferResult.Success">If the operation is successful.</see>
        ///   <see cref="OfferResult.AlreadOffered">If the user has already offered for the same trade.</see>
        ///   <see cref="OfferResult.OwnerCannotOffer">If the user is the owner of the trade.</see>
        /// </returns>
        public OfferResult Offer(int tradeId, ApplicationUser user)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            // the user is the owner
            if (user.Id == trade.Owner.Id)
            {
                return OfferResult.OwnerCannotOffer;
            }

            // the user has not yet offered for this trade
            if (trade.Offers.All(o => o.Id != user.Id))
            {
                trade.Offers.Add(user);

                SendMessage(trade.Owner, new OfferReceivedMessage());

                _tradesRepository.Update(trade);
                _tradesRepository.SaveChanges();

                return OfferResult.Success;
            }

            return OfferResult.AlreadOffered;
        }

        /// <summary>
        /// Withdraws the given user from specified trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <c>True</c> if the offer was successfully removed, <c>false</c> otherwise.
        /// </returns>
        public bool Withdraw(int tradeId, string userId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            if (userId == trade.Owner.Id)
            {
                return false;
            }

            var user = trade.Offers.FirstOrDefault(o => o.Id == userId);

            trade.Offers.Remove(user);

            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return true;
        }

        /// <summary>
        /// Marks the user with the specified user id as the winner for given trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="currentUserId">The user identifier of the user who is invoking the operation.</param>
        /// <returns>
        ///   <c>True</c> if the operation was successful, <c>false</c> otherwise.
        /// </returns>
        public bool ChooseWinner(int tradeId, string userId, string currentUserId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            if (currentUserId != trade.Owner.Id) // the user is *not* entitled to choose the winner!
            {
                return false;
            }

            // sends a message to the winner.
            var winner = FindUserById(userId);
            SendMessage(winner, new TradeWonMessage());

            // sends a message to those who have not win.
            SendMessage(
                    trade.Offers.Where(u => u.Id != winner.Id),
                    () => new TradeLostMessage());

            trade.Winner = winner;
            trade.State = TradeStates.Closed;

            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return true;
        }

        /// <summary>
        /// Marks the trade with the specified id as completed.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The updated trade object.
        /// </returns>
        /// <remarks>
        /// A request for feedback is added to the inbox of the specified user.
        /// </remarks>
        public Trade MarkAsCompleted(int tradeId, ApplicationUser user)
        {
            var model = _tradesRepository.GetSingle(tradeId);
            
            model.State = TradeStates.Completed;
            model.Feedback = new TradeFeedback();

            SendMessage(user, new FeedbackRequestMessage(model.Id));

            _tradesRepository.Update(model);
            _tradesRepository.SaveChanges();

            return model;
        }

        /// <summary>
        /// The specified user leaves a feedback score for the given trade.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <param name="score">The score.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        /// <see cref="LeaveFeedbackResult.Ok">If the operation is successful.</see>
        /// <see cref="LeaveFeedbackResult.AlreadyLeft">If the user has already left a feedback for the trade.</see>
        /// <see cref="LeaveFeedbackResult.Unauthorized">If the user is not authorized to leave a feedback for the trade.</see>
        /// </returns>
        public LeaveFeedbackResult LeaveFeedback(int tradeId, int score, ApplicationUser user)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            var result = CanLeaveFeedback(trade, user.Id);
            if (result != LeaveFeedbackResult.Ok)
            {
                return result;
            }

            if (user.Id == trade.Owner.Id)
            {
                trade.Feedback.Owner = true;

                AddFeedback(trade.Winner, score);
            }
            else
            {
                trade.Feedback.Winner = true;

                AddFeedback(trade.Owner, score);
            }

            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return LeaveFeedbackResult.Ok;
        }

        /// <summary>
        /// The owner of the trade proposes the details for the exchange.
        /// </summary>
        /// <param name="winner">The winner.</param>
        /// <param name="details">The exchange details.</param>
        /// <returns>
        /// The resulting message.
        /// </returns>
        public Message ExchangeManagementConfirmed(ApplicationUser winner, ExchangeDetails details)
        {
            var message = new ExchangeDetailsMessage(details);
            SendMessage(winner, message);

            return message;
        }

        #region Private methods

        private void AddFeedback(ApplicationUser receiver, int score)
        {
            Debug.Assert(receiver != null);
            Debug.Assert(score >= 1 && score <= 5);
            
            var feedback = new Feedback(score);
            
            receiver.Feedbacks.Add(feedback);
            SendMessage(receiver, new FeedbackReceivedMessage(feedback));
        }

        private static LeaveFeedbackResult CanLeaveFeedback(Trade trade, string userId)
        {
            Debug.Assert(trade != null);
            Debug.Assert(!string.IsNullOrWhiteSpace(userId));
            
            if (userId != trade.Owner.Id && userId != trade.Winner.Id)
            {
                return LeaveFeedbackResult.Unauthorized;
            }

            if (userId == trade.Owner.Id && trade.Feedback.Owner ||
                userId == trade.Winner.Id && trade.Feedback.Winner)
            {
                return LeaveFeedbackResult.AlreadyLeft;
            }
            
            return LeaveFeedbackResult.Ok;
        }

        private void UpdateUser(ApplicationUser user)
        {
            Debug.Assert(user != null);

            _userStore.UpdateAsync(user).Wait();
        }

        private ApplicationUser FindUserById(string userId)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(userId));

            return _userStore.FindByIdAsync(userId).Result;
        }

        private void SendMessage(IEnumerable<ApplicationUser> users, Func<Message> createMsgFunc) // "Func" otherwise only 1 message is saved on the db
        {
            Debug.Assert(users != null);
            Debug.Assert(createMsgFunc != null);

            foreach (var user in users)
            {
                AddNotifications(user, createMsgFunc());
            }
        }

        private void SendMessage(ApplicationUser user, Message msg)
        {
            Debug.Assert(user != null);
            Debug.Assert(msg != null);

            AddNotifications(user, msg);
        }

        private void AddNotifications(ApplicationUser user, Message m)
        {
            Debug.Assert(user != null);
            Debug.Assert(m != null);

            AddUINotification(user, m);
            //AddEmailNotification(user, m);
        }

        private void AddUINotification(ApplicationUser user, Message m)
        {
            Debug.Assert(user != null);
            Debug.Assert(m != null);

            user.Messages.Add(m);

            UpdateUser(user);
        }

        #endregion
        
        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;

        private readonly IRepository<Item> _itemsRepository;

        private readonly IUserStore<ApplicationUser> _userStore;

        #endregion
    }

    public enum OfferResult
    {
        Success,
        AlreadOffered,
        OwnerCannotOffer
    }

    public enum LeaveFeedbackResult
    {
        Ok,
        Unauthorized,
        AlreadyLeft
    }
}