namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class TradeWonMessage : MessageForATrade
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeWonMessage"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        public TradeWonMessage(int tradeId)
            : base(tradeId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeWonMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected TradeWonMessage()
        {
        }

        #endregion

        public override string Subject
        {
            get { return "Trade Won!"; }
        }

        public override string Text
        {
            get
            {
                return "You've just won a <a href='/Trades/Details?id=" + TradeId + "'>trade</a>. Congratulations!";
            }
        }
    }
}