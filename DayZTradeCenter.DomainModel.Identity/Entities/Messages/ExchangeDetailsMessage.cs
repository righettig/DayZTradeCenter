using System;
using System.ComponentModel.DataAnnotations;

namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class ExchangeDetailsMessage : Message
    {
        #region Ctors

        public ExchangeDetailsMessage()
        {
        }

        public ExchangeDetailsMessage(ExchangeDetails details)
            : base(string.Format("My SteamId is {0}. Meet me at {1}, server {2}, time {3} GTM",
                details.SteamId,
                details.Location,
                details.Server,
                details.Time))
        {
            Details = details;
        }

        #endregion

        public override string Subject
        {
            get { return "Exchange details received"; }
        }

        public virtual ExchangeDetails Details { get; set; }

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