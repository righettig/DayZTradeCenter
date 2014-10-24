namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class OfferReceivedMessage : MessageForATrade
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferReceivedMessage"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        public OfferReceivedMessage(int tradeId)
            : base(tradeId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OfferReceivedMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected OfferReceivedMessage()
        {
        }

        #endregion
        
        public override string Subject
        {
            get { return "New offer received!"; }
        }

        public override string Text
        {
            get
            {
                return "You've received an <a href='/Trades/Details?id=" + TradeId +
                       "'>offer</a> for one of your trades.";
            }
        }
    }
}