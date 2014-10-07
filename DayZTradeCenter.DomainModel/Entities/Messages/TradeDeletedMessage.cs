namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class TradeDeletedMessage : Message
    {
        public override string Subject
        {
            get { return "Trade deleted :("; }
        }

        public override string Text
        {
            get { return "A trade you've offered to has been deleted."; }
        }
    }
}