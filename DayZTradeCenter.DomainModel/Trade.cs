using System;
using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Identity.Entities;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
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

            Offers = new List<IApplicationUser>();

            State = TradeStates.Active;
        }

        #region Public properties

        public int Id { get; set; }

        public DateTime CreationDate { get; set; }
        public TradeStates State { get; set; }

        public ICollection<TradeDetails> Have { get; private set; }
        public ICollection<TradeDetails> Want { get; private set; }

        public ICollection<IApplicationUser> Offers { get; private set; }

        public IApplicationUser Owner { get; set; }
        public IApplicationUser Winner { get; set; }

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

            _item = item;
            _quantity = quantity;
        }

        public Item Item
        {
            get { return _item; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }

        #region Private fields

        private readonly Item _item;
        private readonly int _quantity;

        #endregion
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
