using System.Collections.Generic;
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
        public DashboardViewModel(
            IEnumerable<Trade> latestTrades, 
            IEnumerable<Trade> hotTrades) 
            : base(latestTrades, hotTrades)
        {
        }

        public float? Reputation { get; set; }
        
        public IEnumerable<Trade> MyTrades { get; set; }
        public IEnumerable<Trade> MyOffers { get; set; }
        
        public IEnumerable<EventInfo> History { get; set; }

        /// <summary>
        /// Gets or sets the target reputation.
        /// </summary>
        /// <value>
        /// The target reputation, i.e., the reputation of the next user in the reputation-based ranking.
        /// </value>
        public float? TargetReputation { get; set; }

        /// <summary>
        /// Gets or sets the ranking.
        /// </summary>
        /// <value>
        /// The reputation ranking.
        /// </value>
        public int Ranking { get; set; }
    }
}