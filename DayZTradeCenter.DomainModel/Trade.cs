using System;
using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Identity.Entities;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.DomainModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

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
        }

        #region Public properties

        public int Id { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public ICollection<TradeDetails> Have { get; private set; }
        public ICollection<TradeDetails> Want { get; private set; }
        
        public ICollection<IApplicationUser> Offers { get; private set; }
        
        public IApplicationUser Owner { get; set; }
        
        public string Winner { get; set; }
        
        public bool IsClosed
        {
            get { return Winner != null; }
        }

        public bool Completed { get; set; }
        public bool FeedbackReceived { get; set; }

        #endregion
    }
}
