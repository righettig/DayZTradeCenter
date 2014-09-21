using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DayZTradeCenter.UI.Web.Models
{
    public class CreateTradeViewModel
    {
        [Display(Name = "Have")]
        public int HaveId { get; set; }

        [Display(Name = "Want")]
        public int WantId { get; set; }

        public SelectList Items { get; set; }
    }
}