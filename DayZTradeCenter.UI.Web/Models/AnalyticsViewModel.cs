using System.Collections.Generic;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.UI.Web.Models
{
    public class AnalyticsViewModel
    {
        public class ItemDetails
        {
            public Item Item { get; set; }
            public int Count { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsViewModel"/> class.
        /// </summary>
        public AnalyticsViewModel()
        {
            MostWantedItems = new List<ItemDetails>();    
            MostOfferedItems = new List<ItemDetails>();    
        }

        public IEnumerable<ItemDetails> MostWantedItems { get; set; }
        public IEnumerable<ItemDetails> MostOfferedItems { get; set; }
    }
}