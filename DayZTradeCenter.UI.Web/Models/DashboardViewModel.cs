using System.Collections.Generic;
using DayZTradeCenter.DomainModel;

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

        public float Reputation { get; set; }
        
        public IEnumerable<Trade> MyTrades { get; set; }
        public IEnumerable<Trade> MyOffers { get; set; }
        
        public IEnumerable<EventInfo> History { get; set; }
    }
}