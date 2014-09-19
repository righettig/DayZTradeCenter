using System.ComponentModel.DataAnnotations;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class ProfileViewModel : ExternalLoginConfirmationViewModel
    {
        public string Id { get; set; }
        public float Reputation { get; set; }
    }
}
