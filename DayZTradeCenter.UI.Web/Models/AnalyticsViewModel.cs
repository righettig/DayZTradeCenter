using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.UI.Web.Helpers;
using DotNet.Highcharts;

namespace DayZTradeCenter.UI.Web.Models
{
    public class AnalyticsViewModel
    {
        public AnalyticsViewModel(
            ItemCategories? getMostWantedCategory, 
            ItemCategories? getMostOfferedCategory, 
            ItemSubcategories? getMostWantedSubcategory, 
            ItemSubcategories? getMostOfferedSubcategory)
        {
            MostWantedItems = new List<ItemDetails>();
            MostOfferedItems = new List<ItemDetails>();

            // from enum to string.
            if (getMostWantedCategory != null)
            {
                _getMostWantedCategory = getMostWantedCategory.Value.GetDescription();
            }

            if (getMostOfferedCategory != null)
            {
                _getMostOfferedCategory = getMostOfferedCategory.Value.GetDescription();
            }

            if (getMostWantedSubcategory != null)
            {
                _getMostWantedSubcategory = getMostWantedSubcategory.Value.GetDescription();
            }

            if (getMostOfferedSubcategory != null)
            {
                _getMostOfferedSubcategory = getMostOfferedSubcategory.Value.GetDescription();
            }
        }

        #region Public properties

        public IEnumerable<ItemDetails> MostWantedItems { get; set; }
        public IEnumerable<ItemDetails> MostOfferedItems { get; set; }

        /// <summary>
        /// Gets the most wanted category.
        /// </summary>
        /// <value>
        /// The most wanted category.
        /// </value>
        public string MostWantedCategory
        {
            get { return _getMostWantedCategory; }
        }

        /// <summary>
        /// Gets the most offered category.
        /// </summary>
        /// <value>
        /// The most offered category.
        /// </value>
        public string MostOfferedCategory
        {
            get { return _getMostOfferedCategory; }
        }

        /// <summary>
        /// Gets the most wanted subcategory.
        /// </summary>
        /// <value>
        /// The most wanted subcategory.
        /// </value>
        public string MostWantedSubcategory
        {
            get { return _getMostWantedSubcategory; }
        }

        /// <summary>
        /// Gets the most offered subcategory.
        /// </summary>
        /// <value>
        /// The most offered subcategory.
        /// </value>
        public string MostOfferedSubcategory
        {
            get { return _getMostOfferedSubcategory; }
        }

        // http://offering.solutions/2014/05/09/how-to-include-dotnet-highcharts-in-asp-net-mvc-with-viewmodels/
        public Highcharts Chart { get; set; }

        [Display(Name = "Item")]
        public int ItemId { get; set; }

        public SelectList Items { get; set; }

        #endregion
        
        #region Private fields

        private readonly string _getMostWantedCategory;
        private readonly string _getMostOfferedCategory;
        private readonly string _getMostWantedSubcategory;
        private readonly string _getMostOfferedSubcategory;

        #endregion
    }
}