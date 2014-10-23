namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class FeedbackRequestMessageForATrade : MessageForATrade
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessageForATrade"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        public FeedbackRequestMessageForATrade(int tradeId)
            : base(tradeId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessageForATrade"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected FeedbackRequestMessageForATrade()
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
    }
}