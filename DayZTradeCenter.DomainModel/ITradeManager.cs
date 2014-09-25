using System.Collections.Generic;

namespace DayZTradeCenter.DomainModel
{
    public interface ITradeManager
    {
        IEnumerable<Trade> GetLatestTrades(int count = 12);
        IEnumerable<Trade> GetHottestTrades(int count = 12);

        IEnumerable<Trade> GetTradesByUser(string userId);
        IEnumerable<Trade> GetOffersByUser(string userId);
    }
}
