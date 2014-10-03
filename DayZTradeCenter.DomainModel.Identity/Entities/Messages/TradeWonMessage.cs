namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class TradeWonMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeWonMessage"/> class.
        /// </summary>
        public TradeWonMessage()
            : base("You've just won a trade. Congratulations!")
        {
        }

        public override string Subject
        {
            get { return "Trade Won!"; }
        }
    }
}