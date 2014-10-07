using System;
using System.ComponentModel.DataAnnotations;

namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class ExchangeDetailsMessage : Message
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeDetailsMessage"/> class.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <exception cref="System.ArgumentNullException">details</exception>
        public ExchangeDetailsMessage(ExchangeDetails details)
        {
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            Details = details;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeDetailsMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected ExchangeDetailsMessage()
        {
        }

        #endregion

        #region Public properties

        public override string Subject
        {
            get { return "Exchange details received"; }
        }

        public override string Text
        {
            get
            {
                return string.Format("My SteamId is {0}. Meet me at {1}, server {2}, time {3} GTM",
                    Details.SteamId,
                    Details.Location,
                    Details.Server,
                    Details.Time);
            }
        }

        public virtual ExchangeDetails Details { get; private set; }

        #endregion
    }

    public class ExchangeDetails
    {
        public int Id { get; set; }

        public string SteamId { get; set; }
        public string Location { get; set; }
        public string Server { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}