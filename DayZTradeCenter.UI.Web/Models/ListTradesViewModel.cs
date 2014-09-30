using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel;

namespace DayZTradeCenter.UI.Web.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListTradesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListTradesViewModel"/> class.
        /// </summary>
        /// <param name="canCreate">if set to <c>true</c> the user is able to create a Trade.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="trades">The trades.</param>
        /// <param name="canCreateANewTrade">if set to <c>true</c> the user is able to create a new Trade.</param>
        public ListTradesViewModel(bool canCreate, string userId, IEnumerable<Trade> trades, bool canCreateANewTrade)
        {
            _canCreate = canCreate;
            _canCreateANewTrade = canCreateANewTrade;

            _trades = new List<TradeViewModel>();

            foreach (var trade in trades)
            {
                _trades.Add(new TradeViewModel
                {
                    TradeData = trade,
                    CanOffer =
                        canCreate &&
                        userId != trade.Owner.Id &&
                        trade.Offers.All(o => o.Id != userId)
                });
            }
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
        public IEnumerable<TradeViewModel> Trades
        {
            get { return _trades; }
        }

        public IEnumerable<ItemViewModel> Items { get; set; }

        #endregion
        
        #region Private fields

        private readonly bool _canCreate;
        private readonly bool _canCreateANewTrade;
        
        private readonly List<TradeViewModel> _trades;
        
        #endregion
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