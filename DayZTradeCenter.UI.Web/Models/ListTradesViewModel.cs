using System.Collections.Generic;
using DayZTradeCenter.DomainModel.Entities;
using PagedList;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ListTradesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListTradesViewModel" /> class.
        /// </summary>
        /// <param name="canCreate">if set to <c>true</c> the user is able to create a Trade.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="trades">The trades.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="canCreateANewTrade">if set to <c>true</c> the user is able to create a new Trade.</param>
        /// <param name="search">if set to <c>true</c> the user has done a search.</param>
        public ListTradesViewModel(
            bool canCreate, string userId, 
            IEnumerable<Trade> trades, int pageNumber, int pageSize, 
            bool canCreateANewTrade, bool search)
        {
            _canCreate = canCreate;
            _canCreateANewTrade = canCreateANewTrade;
            _search = search;

            _tradesTableViewModel = new TradeTableViewModel(trades, pageNumber, pageSize, userId, canCreate);
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

        /// <summary>
        /// Gets the view models for the trades.
        /// </summary>
        /// <value>
        /// The trades view models.
        /// </value>
        public IPagedList<TradeViewModel> Trades
        {
            get { return _tradesTableViewModel.Trades; }
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

    /// <summary>
    /// A Trade view model.
    /// </summary>
    public class TradeViewModel
    {
        public bool CanOffer { get; set; }
        public Trade TradeData { get; set; }
    }
}