using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DayZTradeCenter.DomainModel.Identity.Entities.Messages;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel.Identity.Entities
{
    /// <summary>
    /// The application user.
    /// </summary>
    public interface IApplicationUser
    {
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the reputation.
        /// </summary>
        /// <returns></returns>
        float GetReputation();

        ICollection<Feedback> Feedbacks { get; set; }

        ICollection<Message> Messages { get; set; }
    }

    public class ApplicationUser : IdentityUser, IApplicationUser
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
        /// <returns></returns>
        public float GetReputation()
        {
            if (Feedbacks == null || Feedbacks.Count == 0)
            {
                return 0;
            }

            return (float) Feedbacks.Average(f => f.Score);
        }

        // NB: "virtual" to enable EF lazy loading.
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }

    public class Feedback
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int TradeId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the user the feedback is coming from.
        /// </summary>
        /// <value>
        /// The identifier of the user the feedback is coming from.
        /// </value>
        public string From { get; set; }

        public int Score { get; set; }
    }
}