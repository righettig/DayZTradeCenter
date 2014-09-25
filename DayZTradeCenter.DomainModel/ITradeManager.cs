using System.Collections.Generic;

namespace DayZTradeCenter.DomainModel
{
    public interface ITradeManager
    {
        IEnumerable<Trade> GetLatestTrades(int count = 5);
    }
}
