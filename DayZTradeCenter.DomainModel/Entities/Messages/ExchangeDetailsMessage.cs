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
                const string text = "My SteamId is {0}. Meet me at <a href=\"http://www.izurvive.com/#c=" +
                                    "{1}\" target='_blank'>{1}</a>, server {2}, time {3} GTM";

                return string.Format(text,
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

        [Required]
        public string SteamId { get; set; }
        
        [Required, MaxLength(64)]
        [RegularExpression("^(([0-9])+;([0-9])+;([0-9])+)+$", ErrorMessage = "Location is required and must be properly formatted 'x;y;z'.")]
        public string Location { get; set; }
        
        [Required, MaxLength(32)]
        public string Server { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}