namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class TradeLostMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeLostMessage"/> class.
        /// </summary>
        public TradeLostMessage()
            : base("You've just lost a trade. We're sorry for you :(")
        {
        }

        public override string Subject
        {
            get { return "Trade Lost :("; }
        }
    }
}