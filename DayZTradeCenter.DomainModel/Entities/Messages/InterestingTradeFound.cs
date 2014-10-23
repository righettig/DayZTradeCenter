namespace DayZTradeCenter.DomainModel.Entities.Messages
{
    public class InterestingTradeFound : MessageForATrade
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="InterestingTradeFound"/> class.
        /// </summary>
        /// <param name="tradeId">The trade identifier.</param>
        public InterestingTradeFound(int tradeId)
            : base(tradeId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeDetailsMessage"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF.
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected InterestingTradeFound()
        {
        }

        #endregion
        
        public override string Subject
        {
            get { return "Interesting Trade found!"; }
        }

        public override string Text
        {
            get
            {
                return "We've found a <a href='/Trades/Details?id=" + TradeId +
                       "'>trade</a> you might be interested into. Check it out!";
            }
        }
    }
}
