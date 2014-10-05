using System;
using System.Collections.Generic;

namespace DayZTradeCenter.DomainModel.Interfaces
{
    public interface IAnalyticsProvider
    {
        IEnumerable<ItemDetails> GetMostWantedItem();
        IEnumerable<ItemDetails> GetMostOfferedItem();
        IEnumerable<TrendsResult> GetDailyTrendsFor(int itemId, TrendsType type);
        IEnumerable<Item> GetAllItems();
    }

    public class ItemDetails
    {
        public Item Item { get; set; }
        public int Count { get; set; }
    }

    public enum TrendsType
    {
        H,
        W,
        Both
    }

    public class TrendsResult
    {
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
    }
}
