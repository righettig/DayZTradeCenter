using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Identity.Entities;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    public class TradeManager : ITradeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeManager"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <param name="itemsRepository">The items repository.</param>
        /// <exception cref="System.ArgumentNullException">
        /// tradesRepository
        /// or
        /// itemsRepository
        /// </exception>
        public TradeManager(
            IRepository<Trade> tradesRepository, IRepository<Item> itemsRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            if (itemsRepository == null)
            {
                throw new ArgumentNullException("itemsRepository");
            }

            _tradesRepository = tradesRepository;
            _itemsRepository = itemsRepository;
        }

        private IQueryable<Trade> All
        {
            get { return _tradesRepository.GetAll(); }
        }
    
        public IEnumerable<Trade> GetLatestTrades(int count = 12)
        {
            return All
                .OrderByDescending(trade => trade.CreationDate)
                .Take(count);
        }

        public IEnumerable<Trade> GetHottestTrades(int count = 12)
        {
            return All
                .OrderByDescending(trade => trade.Offers.Count)
                .Take(count);
        }

        public IEnumerable<Trade> GetAllTrades()
        {
            return All;
        }

        public IEnumerable<Trade> GetTradesByUser(string userId)
        {
            return All.Where(t => t.Owner.Id == userId);
        }

        public IEnumerable<Trade> GetOffersByUser(string userId)
        {
            return All.Where(t => t.Offers.Any(o => o.Id == userId));
        }

        public Trade GetTradeById(int tradeId)
        {
            return _tradesRepository.GetSingle(tradeId);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemsRepository.GetAll();
        }

        public bool CanCreateTrade(string userId)
        {
            // the active trades of the specified user. 
            var trades =
                All.Where(
                    trade => trade.Owner.Id == userId && trade.IsClosed == false);

            return trades.Count() < 3;
        }

        public bool CreateNewTrade(IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, IApplicationUser user)
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

            // TODO: use TimeProvider
            trade.CreationDate = DateTime.Now;

            _tradesRepository.Insert(trade);
            _tradesRepository.SaveChanges();

            return true;
        }

        public bool Offer(int tradeId, IApplicationUser user)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            // user has not yet offered for this trade
            if (trade.Offers.All(o => o.Id != user.Id))
            {
                trade.Offers.Add(user);

                _tradesRepository.Update(trade);
                _tradesRepository.SaveChanges();

                return true;
            }

            return false;
        }

        public bool Withdraw(int tradeId, string userId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            var user = trade.Offers.FirstOrDefault(o => o.Id == userId);

            trade.Offers.Remove(user);

            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return true;
        }

        public bool ChooseWinner(int tradeId, string userId)
        {
            var model = _tradesRepository.GetSingle(tradeId);

            model.Winner = userId;

            _tradesRepository.Update(model);
            _tradesRepository.SaveChanges();

            return true;
        }

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
                Timestamp = DateTime.Now,
                Score = score,
                TradeId = tradeId
            });

            return true;
        }

        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;
        private readonly IRepository<Item> _itemsRepository;

        #endregion
    }
}