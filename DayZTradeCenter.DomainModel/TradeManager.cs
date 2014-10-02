using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using Microsoft.AspNet.Identity;
using rg.GenericRepository.Core;
using rg.Time;

namespace DayZTradeCenter.DomainModel
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
            return All.Where(t => t.IsClosed == false);
        }

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        /// <param name="params">The search parameters.</param>
        /// <returns>
        /// The active trades that satisfy the search parameters.
        /// </returns>
        public IEnumerable<Trade> GetActiveTrades(SearchParams @params)
        {
            IEnumerable<Trade> result;

            if (@params.ItemId.HasValue)
            {
                var trades = GetActiveTrades();

                switch (@params.SearchType)
                {
                    case "have":
                        result = trades
                            .Where(t => t.Have.Any(i => i.Item.Id == @params.ItemId));
                        break;

                    case "want":
                        result = trades
                            .Where(t => t.Want.Any(i => i.Item.Id == @params.ItemId));
                        break;

                    default:
                        result = trades
                            .Where(t =>
                                t.Have.Any(i => i.Item.Id == @params.ItemId) ||
                                t.Want.Any(i => i.Item.Id == @params.ItemId));
                        break;
                }
            }
            else
            {
                result = GetActiveTrades();
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
                    .Where(trade => trade.IsClosed == false);

            return activeTrades.Count() < 3;
        }

        /// <summary>
        /// Creates a new trade.
        /// </summary>
        /// <param name="have">The items for the "Have" section.</param>
        /// <param name="want">The items for the "Want" section.</param>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>True</c> if the trade was successfully created, <c>false</c> otherwise.
        /// </returns>
        public bool CreateNewTrade(
            IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, IApplicationUser user)
        {
            var trade = new Trade();

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
                foreach (var user in trade.Offers
                    .Select(u => u.Id)
                    .Select(id => _userStore.FindByIdAsync(id).Result))
                {
                    user.Messages.Add(new Message("A trade you've offered to has been deleted."));

                    _userStore.UpdateAsync(user);
                }

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
        public OfferResult Offer(int tradeId, IApplicationUser user)
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

                var owner = _userStore.FindByIdAsync(trade.Owner.Id).Result;
                owner.Messages.Add(new Message("You've received an offer for one of your trades."));

                _userStore.UpdateAsync(owner);

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
        /// <returns>
        ///   <c>True</c> if the operation was successful, <c>false</c> otherwise.
        /// </returns>
        public bool ChooseWinner(int tradeId, string userId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            trade.Winner = userId;

            // sends a message to the winner.
            var winner = _userStore.FindByIdAsync(userId).Result;
            winner.Messages.Add(new Message("You've just won a trade. Congratulations!"));

            _userStore.UpdateAsync(winner).Wait();

            // sends a message to those who have not win.
            foreach (
                var loser in
                    from id in trade.Offers.Select(o => o.Id)
                    where id != winner.Id
                    select _userStore.FindByIdAsync(id).Result)
            {
                loser.Messages.Add(new Message("You've just lost a trade. We're sorry for you :("));

                _userStore.UpdateAsync(loser);
            }

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
        public Trade MarkAsCompleted(int tradeId, IApplicationUser user)
        {
            var model = _tradesRepository.GetSingle(tradeId);
            model.Completed = true;

            if (user.Messages == null)
            {
                user.Messages = new List<Message>();
            }

            var message = new FeedbackRequestMessage { TradeId = model.Id };
            user.Messages.Add(message);

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
        ///   <c>True</c> if the operation was successful, <c>false</c> otherwise.
        /// </returns>
        public bool LeaveFeedback(int tradeId, int score, IApplicationUser user)
        {
            var model = _tradesRepository.GetSingle(tradeId);

            if (user.Feedbacks == null)
            {
                user.Feedbacks = new List<Feedback>();
            }

            user.Feedbacks.Add(new Feedback
            {
                From = model.Owner.Id,
                Timestamp = TimeProvider.Now,
                Score = score,
                TradeId = tradeId
            });

            model.FeedbackReceived = true;
            
            _tradesRepository.Update(model);
            _tradesRepository.SaveChanges();

            return true;
        }

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
}