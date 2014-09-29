using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Identity.Entities;

namespace DayZTradeCenter.DomainModel
{
    public interface ITradeManager
    {
        IEnumerable<Trade> GetLatestTrades(int count = 12);
        IEnumerable<Trade> GetHottestTrades(int count = 12);
        IEnumerable<Trade> GetAllTrades();

        IEnumerable<Trade> GetTradesByUser(string userId);
        IEnumerable<Trade> GetOffersByUser(string userId);

        Trade GetTradeById(int tradeId);

        IEnumerable<Item> GetAllItems();

        bool CanCreateTrade(string userId);

        bool CreateNewTrade(IEnumerable<ItemViewModel> have, IEnumerable<ItemViewModel> want, IApplicationUser user);

        bool Offer(int tradeId, IApplicationUser user);
        bool Withdraw(int tradeId, string userId);

        bool ChooseWinner(int tradeId, string userId);

        Trade MarkAsCompleted(int tradeId, IApplicationUser user);

        bool LeaveFeedback(int tradeId, int score, IApplicationUser user);
    }
}
