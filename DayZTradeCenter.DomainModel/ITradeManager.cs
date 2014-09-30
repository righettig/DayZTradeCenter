using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Identity.Entities;

namespace DayZTradeCenter.DomainModel
{
    public class SearchParams
    {
        public int? ItemId { get; set; }
        public string SearchType { get; set; }
    }

    public interface ITradeManager
    {
        /// <summary>
        /// Gets the latest active trades ordered by creation date.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns></returns>
        IEnumerable<Trade> GetLatestTrades(int count = 12);

        /// <summary>
        /// Gets the hottest active trades ordered by number of offers.
        /// </summary>
        /// <param name="count">How many results.</param>
        /// <returns></returns>
        IEnumerable<Trade> GetHottestTrades(int count = 12);

        /// <summary>
        /// Gets the active trades.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Trade> GetActiveTrades();
        
        IEnumerable<Trade> GetActiveTrades(SearchParams @params);


        IEnumerable<Trade> GetTradesByUser(string userId);
        IEnumerable<Trade> GetOffersByUser(string userId);

        Trade GetTradeById(int tradeId);

        IEnumerable<Item> GetAllItems();

        bool CanCreateTrade(string userId);

        bool CreateNewTrade(IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, IApplicationUser user);

        bool DeleteTrade(int tradeId, string userId);

        bool Offer(int tradeId, IApplicationUser user);
        bool Withdraw(int tradeId, string userId);

        bool ChooseWinner(int tradeId, string userId);

        Trade MarkAsCompleted(int tradeId, IApplicationUser user);

        bool LeaveFeedback(int tradeId, int score, IApplicationUser user);
    }
}
