using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DayZTradeCenter.DomainModel.Entities.Messages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Gets the reputation.
        /// </summary>
        /// <returns>A floating point value representing the reputation.</returns>
        public virtual float GetReputation() // 'virtual' to be easily stubbed.
        {
            if (Feedbacks == null || Feedbacks.Count == 0)
            {
                return 0;
            }

            return (float) Feedbacks.Average(f => f.Score);
        }

        // NB: "virtual" to enable EF lazy loading.
        public virtual ICollection<Feedback> Feedbacks { get; private set; }
        public virtual ICollection<Message> Messages { get; private set; }

        public bool IsApproved { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
    }
}