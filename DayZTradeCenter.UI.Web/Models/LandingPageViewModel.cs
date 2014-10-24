using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;

namespace DayZTradeCenter.UI.Web.Models
{
    public class LandingPageViewModel
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageViewModel"/> class.
        /// </summary>
        /// <param name="latestTrades">The latest trades.</param>
        /// <param name="hotTrades">The hottest trades.</param>
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

            LatestTrades = latestTrades.Select(t => new TradeGalleryViewModel(t)).ToList();
            HotTrades = hotTrades.Select(t => new TradeGalleryViewModel(t)).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LandingPageViewModel"/> class.
        /// </summary>
        protected LandingPageViewModel()
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the latest trades.
        /// </summary>
        /// <value>
        /// The latest trades.
        /// </value>
        public IEnumerable<TradeGalleryViewModel> LatestTrades { get; protected set; }

        /// <summary>
        /// Gets or sets the hot trades.
        /// </summary>
        /// <value>
        /// The hot trades.
        /// </value>
        public IEnumerable<TradeGalleryViewModel> HotTrades { get; protected set; }
    }

    public class TradeGalleryViewModel
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeGalleryViewModel" /> class.
        /// </summary>
        /// <param name="trade">The trade.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="alreadyOffered">if set to <c>true</c> the user has already offered to this trade.</param>
        /// <exception cref="System.ArgumentNullException">trade</exception>
        public TradeGalleryViewModel(Trade trade, string currentUserId, bool alreadyOffered)
        {
            if (trade == null)
            {
                throw new ArgumentNullException("trade");
            }

            _trade = trade;

            _canOffer =
                trade.Owner.Id != currentUserId && !alreadyOffered;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeGalleryViewModel"/> class.
        /// </summary>
        /// <param name="trade">The trade.</param>
        public TradeGalleryViewModel(Trade trade)
            : this(trade, null, false)
        {
        }

        #endregion
        
        public Trade Trade
        {
            get { return _trade; }
        }

        public bool CanOffer
        {
            get { return _canOffer; }
        }

        #region Private fields

        private readonly Trade _trade;
        private readonly bool _canOffer;

        #endregion
    }
}