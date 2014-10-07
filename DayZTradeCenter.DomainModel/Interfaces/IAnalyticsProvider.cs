using System;
using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Entities;

namespace DayZTradeCenter.DomainModel.Interfaces
{
    public interface IAnalyticsProvider
    {
        IEnumerable<ItemDetails> GetMostWantedItem();
        IEnumerable<ItemDetails> GetMostWantedItem(DateTime start, DateTime end);

        IEnumerable<ItemDetails> GetMostOfferedItem();
        IEnumerable<ItemDetails> GetMostOfferedItem(DateTime start, DateTime end);

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
        /// <summary>
        /// Initializes a new instance of the <see cref="TrendsResult"/> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="count">The count.</param>
        public TrendsResult(DateTime date, int itemId, int count)
        {
            Date = date;
            ItemId = itemId;
            Count = count;
        }

        public DateTime Date { get; private set; }
        public int ItemId { get; private set; }
        public int Count { get; private set; }
    }
}
