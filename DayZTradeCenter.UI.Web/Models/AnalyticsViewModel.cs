using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DotNet.Highcharts;

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

        // http://offering.solutions/2014/05/09/how-to-include-dotnet-highcharts-in-asp-net-mvc-with-viewmodels/
        public Highcharts Chart { get; set; }

        [Display(Name = "Item")]
        public int ItemId { get; set; }
        public SelectList Items { get; set; }
    }
}