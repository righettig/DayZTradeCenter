namespace DayZTradeCenter.UI.Web.Models
{
    public class ProfileViewModel : ExternalLoginConfirmationViewModel
    {
        public string Id { get; set; }
        public bool IsAdmin { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
    }
}