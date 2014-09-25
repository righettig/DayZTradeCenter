using System;
using System.Collections.Generic;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.UI.Web.Models
{
    public class LandingPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageViewModel"/> class.
        /// </summary>
        /// <param name="latestTrades">The latest trades.</param>
        /// <exception cref="System.ArgumentNullException">latestTrades</exception>
        public LandingPageViewModel(IEnumerable<Trade> latestTrades)
        {
            if (latestTrades == null)
            {
                throw new ArgumentNullException("latestTrades");
            }

            LatestTrades = latestTrades;
        }

        /// <summary>
        /// Gets or sets the latest trades.
        /// </summary>
        /// <value>
        /// The latest trades.
        /// </value>
        public IEnumerable<Trade> LatestTrades { get; set; }
    }
}