using System;
using System.Collections.Generic;
using System.Linq;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    public class TradeManager : ITradeManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeManager"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <exception cref="System.ArgumentNullException">tradesRepository</exception>
        public TradeManager(IRepository<Trade> tradesRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            _tradesRepository = tradesRepository;
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

        public IEnumerable<Trade> GetTradesByUser(string userId)
        {
            return All.Where(t => t.Owner.Id == userId);
        }

        public IEnumerable<Trade> GetOffersByUser(string userId)
        {
            return All.Where(t => t.Offers.Any(o => o.Id == userId));
        }

        private readonly IRepository<Trade> _tradesRepository;
    }
}