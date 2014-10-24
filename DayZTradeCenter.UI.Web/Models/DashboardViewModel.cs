using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Services;

namespace DayZTradeCenter.UI.Web.Models
{
    public class DashboardViewModel : LandingPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        /// <param name="latestTrades">The latest trades.</param>
        /// <param name="hotTrades">The hot trades.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="offers">The offers.</param>
        /// <exception cref="System.ArgumentNullException">
        /// latestTrades
        /// or
        /// hotTrades
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// 'currentUserId' cannot be null, empty or made only of whitespaces.;currentUserId</exception>
        public DashboardViewModel(
            IEnumerable<Trade> latestTrades, 
            IEnumerable<Trade> hotTrades, 
            string currentUserId, 
            IEnumerable<Trade> offers)
        {
            if (latestTrades == null)
            {
                throw new ArgumentNullException("latestTrades");
            }

            if (hotTrades == null)
            {
                throw new ArgumentNullException("hotTrades");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                throw new ArgumentException(
                    "'currentUserId' cannot be null, empty or made only of whitespaces.", "currentUserId");
            }

            MyOffers = offers;

            LatestTrades = 
                ToTradeGalleryViewModels(latestTrades, currentUserId);

            HotTrades =
                ToTradeGalleryViewModels(hotTrades, currentUserId);
        }

        public IEnumerable<Trade> MyTrades { get; set; }
        public IEnumerable<Trade> MyOffers { get; set; }
        
        public IEnumerable<EventInfo> History { get; set; }

        public UserStats Stats { get; set; }

        /// <summary>
        /// Converts a collection of Trades to the corresponding collection of view models.
        /// </summary>
        /// <param name="trades">The trades.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <returns>A collection of <see cref="TradeGalleryViewModel"/></returns>
        private IEnumerable<TradeGalleryViewModel> ToTradeGalleryViewModels(
            IEnumerable<Trade> trades, string currentUserId)
        {
            return trades.Select(
                t => new TradeGalleryViewModel(t, currentUserId, MyOffers.Contains(t))).ToList();
        }
    }

    public class UserStats
    {
        /// <summary>
        /// Gets or sets the ranking.
        /// </summary>
        /// <value>
        /// The reputation ranking.
        /// </value>
        public int Ranking { get; set; }

        /// <summary>
        /// Gets or sets the reputation.
        /// </summary>
        /// <value>
        /// The reputation.
        /// </value>
        public float? Reputation { get; set; }

        /// <summary>
        /// Gets or sets the target reputation.
        /// </summary>
        /// <value>
        /// The target reputation, i.e., the reputation of the next user in the reputation-based ranking.
        /// </value>
        public float? TargetReputation { get; set; }
    }
}