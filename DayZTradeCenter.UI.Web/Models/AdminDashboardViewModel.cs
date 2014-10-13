using System.Linq;
using DayZTradeCenter.DomainModel.Entities;

namespace DayZTradeCenter.UI.Web.Models
{
    public class AdminDashboardViewModel
    {
        public IOrderedEnumerable<ApplicationUser> Users { get; set; }
    }
}