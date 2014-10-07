namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class TradeLostMessage : Message
    {
        public override string Subject
        {
            get { return "Trade Lost :("; }
        }

        public override string Text
        {
            get { return "You've just lost a trade. We're sorry for you :("; }
        }
    }
}