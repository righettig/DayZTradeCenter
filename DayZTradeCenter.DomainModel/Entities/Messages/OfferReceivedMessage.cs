namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class OfferReceivedMessage : Message
    {
        public override string Subject
        {
            get { return "New offer received!"; }
        }

        public override string Text
        {
            get { return "You've received an offer for one of your trades."; }
        }
    }
}