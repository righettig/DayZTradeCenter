using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public abstract class Message
    {
        public int Id { get; set; }

        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// </remarks>
        protected Message()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <exception cref="System.ArgumentException">text</exception>
        protected Message(string text)
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
        public virtual string Text
        {
            get { return _text; }
        }

        public DateTime Timestamp { get; set; }

        [NotMapped]
        public abstract string Subject { get; }

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
}