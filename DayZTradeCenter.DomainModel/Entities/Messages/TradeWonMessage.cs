namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class TradeWonMessage : Message
    {
        public override string Subject
        {
            get { return "Trade Won!"; }
        }

        public override string Text
        {
            get { return "You've just won a trade. Congratulations!"; }
        }
    }
}