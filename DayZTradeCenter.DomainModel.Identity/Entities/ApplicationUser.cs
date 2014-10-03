using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

    public class Message
    {
        public int Id { get; set; }

        #region Ctors

        // NB: required by EF
        public Message()
        {
        }

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

            Timestamp = DateTime.Now;
        }

        #endregion

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

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public virtual string Subject
        {
            get { return "Message"; }
        }

        public string GetReceived()
        {
            var span = DateTime.Now - Timestamp;

            if (span < TimeSpan.FromMinutes(1))
            {
                return span.Seconds + " seconds ago";
            }

            if (span < TimeSpan.FromHours(1))
            {
                return span.Minutes + " minutes ago";
            }

            if (span >= TimeSpan.FromHours(1) && span < TimeSpan.FromDays(1))
            {
                return span.Hours + " hours ago";
            }

            return Timestamp.ToString(CultureInfo.InvariantCulture);
        }

        private readonly string _text;
    }

    public class FeedbackRequestMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessage"/> class.
        /// </summary>
        public FeedbackRequestMessage()
            : base("Remember to leave a feedback")
        {
        }

        /// <summary>
        /// Gets or sets the trade identifier of the trade associated with the message.
        /// </summary>
        /// <value>
        /// The trade identifier of the trade the message associated with the message.
        /// </value>
        public int TradeId { get; set; }

        // TradeCompleted(tradeId)

        public override string Subject
        {
            get { return "Feedback request"; }
        }
    }
}