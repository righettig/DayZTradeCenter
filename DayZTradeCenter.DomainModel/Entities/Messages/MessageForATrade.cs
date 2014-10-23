namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public abstract class MessageForATrade : Message
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessageForATrade"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        protected MessageForATrade(int tradeId)
        {
            TradeId = tradeId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRequestMessageForATrade"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected MessageForATrade()
        {
        }
        
        #endregion

        /// <summary>
        /// Gets or sets the trade identifier of the trade associated with the message.
        /// </summary>
        /// <value>
        /// The trade identifier of the trade the message associated with the message.
        /// </value>
        public int TradeId { get; private set; }
    }
}