using System;
using System.Globalization;
using rg.Time;

namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public abstract class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF. 
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected Message()
        {
            Timestamp = DateTime.Now;
        }

        #region Public properties

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public abstract string Subject { get; }

        public abstract string Text { get; }

        #endregion
        
        /// <summary>
        /// Gets a message describing how long ago the message has been received.
        /// </summary>
        public string GetReceived()
        {
            var span = TimeProvider.Now - Timestamp;

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
    }
}