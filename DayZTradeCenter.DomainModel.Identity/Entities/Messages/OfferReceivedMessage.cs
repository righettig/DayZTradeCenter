namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class OfferReceivedMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfferReceivedMessage"/> class.
        /// </summary>
        public OfferReceivedMessage()
            : base("You've received an offer for one of your trades.")
        {
        }

        public override string Subject
        {
            get { return "New offer received!"; }
        }
    }
}