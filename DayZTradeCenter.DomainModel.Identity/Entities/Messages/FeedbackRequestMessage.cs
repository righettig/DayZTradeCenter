namespace DayZTradeCenter.DomainModel.Identity.Entities.Messages
{
    public class FeedbackRequestMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessage"/> class.
        /// </summary>
        public FeedbackRequestMessage()
            : base("Remember to leave a feedback")
        {
        }

        /// <summary>
        /// Gets or sets the trade identifier of the trade associated with the message.
        /// </summary>
        /// <value>
        /// The trade identifier of the trade the message associated with the message.
        /// </value>
        public int TradeId { get; set; }

        public override string Subject
        {
            get { return "Feedback request"; }
        }

        public override string Text
        {
            get
            {
                return base.Text + " <a href='/Trades/TradeCompleted?id=" + TradeId + "'>here</a>";
            }
        }
    }
}