namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class TradeDeletedMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDeletedMessage"/> class.
        /// </summary>
        public TradeDeletedMessage()
            : base("A trade you've offered to has been deleted.")
        {
        }

        public override string Subject
        {
            get { return "Trade deleted :("; }
        }
    }
}