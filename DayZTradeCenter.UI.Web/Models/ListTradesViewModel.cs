using System;
using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ListTradesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListTradesViewModel" /> class.
        /// </summary>
        /// <param name="canCreate">if set to <c>true</c> the user is able to create a Trade.</param>
        /// <param name="tradeTableViewModel">The trade table view model.</param>
        /// <param name="canCreateANewTrade">if set to <c>true</c> the user is able to create a new Trade.</param>
        /// <param name="search">if set to <c>true</c> the user has done a search.</param>
        /// <exception cref="System.ArgumentNullException">tradeTableViewModel</exception>
        public ListTradesViewModel(
            bool canCreate, TradeTableViewModel tradeTableViewModel, bool canCreateANewTrade, bool search)
        {
            if (tradeTableViewModel == null)
            {
                throw new ArgumentNullException("tradeTableViewModel");
            }

            _canCreate = canCreate;
            _canCreateANewTrade = canCreateANewTrade;
            _search = search;
            _tradesTableViewModel = tradeTableViewModel;
        }
        
        #region Public properties

        /// <summary>
        /// Gets a value indicating whether the user can create a Trade.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user can create a Trade; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreate
        {
            get { return _canCreate; }
        }

        /// <summary>
        /// Gets a value indicating whether the user can create a new trade.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user can create a new trade; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreateANewTrade
        {
            get { return _canCreateANewTrade; }
        }

        public TradeTableViewModel Trades
        {
            get { return _tradesTableViewModel; }
        }

        public IEnumerable<ItemViewModel> Items { get; set; }

        public bool IsSearchApplied
        {
            get { return _search; }
        }

        #endregion
        
        #region Private fields

        private readonly bool _canCreate;
        private readonly bool _canCreateANewTrade;
        private readonly bool _search;

        private readonly TradeTableViewModel _tradesTableViewModel;

        #endregion
    }

    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TradeDetailsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeDetailsViewModel" /> class.
        /// </summary>
        /// <param name="details">The details.</param>
        /// <param name="trackedItemIds">The tracked item ids.</param>
        /// <exception cref="System.ArgumentNullException">
        /// details
        /// or
        /// trackedItemIds
        /// </exception>
        public TradeDetailsViewModel(TradeDetails details, IEnumerable<int> trackedItemIds)
        {
            if (details == null)
            {
                throw new ArgumentNullException("details");
            }

            if (trackedItemIds == null)
            {
                throw new ArgumentNullException("trackedItemIds");
            }

            ItemId = details.Item.Id;
            ItemName = details.Item.Name;
            Quantity = details.Quantity;
            IsTracked = trackedItemIds.Contains(details.Item.Id);
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public bool IsTracked { get; set; }	
    }

    /// <summary>
    /// A Trade view model.
    /// </summary>
    public class TradeViewModel
    {
        public int Id { get; set; }
        
        public string OwnerId { get; set; }
        public float OwnerReputation { get; set; }

        public bool CanOffer { get; set; }
        public int OffersCount { get; set; }

        public bool IsExperimental { get; set; }
        public bool IsHardcore { get; set; }

        public IEnumerable<TradeDetailsViewModel> Have { get; set; }
        public IEnumerable<TradeDetailsViewModel> Want { get; set; }
    }
}