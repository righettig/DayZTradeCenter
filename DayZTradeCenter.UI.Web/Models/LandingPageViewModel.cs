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
        /// <param name="hotTrades">The hot trades.</param>
        /// <exception cref="System.ArgumentNullException">
        /// latestTrades
        /// or
        /// hotTrades
        /// </exception>
        public LandingPageViewModel(
            IEnumerable<Trade> latestTrades,
            IEnumerable<Trade> hotTrades)
        {
            if (latestTrades == null)
            {
                throw new ArgumentNullException("latestTrades");
            }

            if (hotTrades == null)
            {
                throw new ArgumentNullException("hotTrades");
            }

            LatestTrades = latestTrades;
            HotTrades = hotTrades;
        }

        /// <summary>
        /// Gets or sets the latest trades.
        /// </summary>
        /// <value>
        /// The latest trades.
        /// </value>
        public IEnumerable<Trade> LatestTrades { get; private set; }

        /// <summary>
        /// Gets or sets the hot trades.
        /// </summary>
        /// <value>
        /// The hot trades.
        /// </value>
        public IEnumerable<Trade> HotTrades { get; private set; }
    }
}