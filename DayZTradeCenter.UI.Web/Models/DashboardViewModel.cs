using System.Collections.Generic;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.UI.Web.Models
{
    public class DashboardViewModel
    {
        public float Reputation { get; set; }
        public bool IsAdmin { get; set; }
        
        public IEnumerable<Trade> MyTrades { get; set; }
        public IEnumerable<Trade> MyOffers { get; set; }

        public IEnumerable<Trade> LatestTrades { get; set; }
    }
}