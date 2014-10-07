namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class FeedbackRequestMessage : Message
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessage"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        public FeedbackRequestMessage(int tradeId)
        {
            TradeId = tradeId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected FeedbackRequestMessage()
        {
        }
        
        #endregion

        public override string Subject
        {
            get { return "Feedback request"; }
        }

        public override string Text
        {
            get { return "Remember to leave a feedback <a href='/Trades/TradeCompleted?id=" + TradeId + "'>here</a>"; }
        }

        /// <summary>
        /// Gets or sets the trade identifier of the trade associated with the message.
        /// </summary>
        /// <value>
        /// The trade identifier of the trade the message associated with the message.
        /// </value>
        public int TradeId { get; private set; }
    }
}