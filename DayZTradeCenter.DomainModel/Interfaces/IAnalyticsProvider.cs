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

        /// <summary>
        /// Gets the "W" score,
        /// i.e., how many active trades have the item with the given itemId in the "W" section
        /// with respect to the total amount of trades.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns>The "W" score of the item, in the range [0,1].</returns>
        float GetWScore(int itemId);

        /// <summary>
        /// Gets the "R" score,
        /// i.e., the "richness" score.
        /// </summary>
        /// <param name="itemIds">The item ids.</param>
        /// <returns>The "R" score of the given set of items.</returns>
        float GetRScore(int[] itemIds);

        /// <summary>
        /// Gets the "T" score,
        /// i.e., the trade "value" based on the current trends.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        /// <returns>The "T" score of the given trade.</returns>
        float GetTScore(int tradeId);
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
