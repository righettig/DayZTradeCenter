using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel.Identity.Entities
{
    public interface IApplicationUser
    {
        string Id { get; }
        float GetReputation();

        ICollection<Feedback> Feedbacks { get; set; }
    }

    public class Feedback
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int TradeId { get; set; }
        public string From { get; set; }
        public int Score { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <exception cref="System.ArgumentException">text</exception>
        public Message(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("text");
            }

            _text = text;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get { return _text; }
        }

        private readonly string _text;
    }

    public class FeedbackRequestMessage : Message
    {
        public FeedbackRequestMessage() 
            : base("Remember to leave a feedback")
        {
        }

        public int TradeId { get; set; }

        // TradeCompleted(tradeId)
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

        public float GetReputation()
        {
            return 0;
        }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}