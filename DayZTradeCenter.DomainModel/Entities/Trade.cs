using System;
using System.Collections.Generic;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel.Entities
{
    /// <summary>
    /// A trade for DayZ game items.
    /// </summary>
    public class Trade : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trade"/> class.
        /// </summary>
        public Trade()
        {
            Have = new List<TradeDetails>();
            Want = new List<TradeDetails>();

            Offers = new List<ApplicationUser>();

            State = TradeStates.Active;

            Feedback = new TradeFeedback();
        }

        #region Public properties

        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public TradeStates State { get; set; }

        public bool IsHardcore { get; set; }

        public virtual ICollection<TradeDetails> Have { get; private set; }
        public virtual ICollection<TradeDetails> Want { get; private set; }

        public virtual ICollection<ApplicationUser> Offers { get; private set; }

        public virtual ApplicationUser Owner { get; set; }
        public virtual ApplicationUser Winner { get; set; }

        public TradeFeedback Feedback { get; set; }

        public bool HasReceivedFeedbackFromOwner
        {
            get { return Feedback != null && Feedback.Owner; }
        }

        public bool HasReceivedFeedbackFromWinner
        {
            get { return Feedback != null && Feedback.Winner; }
        }
        
        #endregion
    }

    /// <summary>
    /// The item details for a trade.
    /// </summary>
    public class TradeDetails
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDetails"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="quantity">The quantity.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        public TradeDetails(Item item, int quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            Item = item;
            Quantity = quantity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDetails"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF. 
        /// It is 'protected' as otherwise (i.e. 'private') the lazy loading does not work.
        /// </remarks>
        protected TradeDetails()
        {
        }

        #endregion
        
        public int Id { get; set; }
        public virtual Item Item { get; private set; }
        public int Quantity { get; private set; }
    }

    public class ItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public enum TradeStates
    {
        /// <summary>
        /// The trade has been published.
        /// </summary>
        Active,

        /// <summary>
        /// The owner has chosen the winner.
        /// </summary>
        Closed,

        /// <summary>
        /// The trade has been completed.
        /// </summary>
        Completed
    }

    // to keep track of pending feedback
    // when the trade gets "completed" it can be deleted
    public class TradeFeedback
    {
        // for timed-feedback
        //public DateTime CreationDate { get; set; }

        public bool Owner { get; set; }
        public bool Winner { get; set; }
    }
}
