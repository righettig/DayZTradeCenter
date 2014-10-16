using System.Collections.Generic;
using System.Linq;
using DayZTradeCenter.DomainModel.Entities;
using PagedList;

namespace DayZTradeCenter.UI.Web.Models
{
    public class TradeTableViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeTableViewModel" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="canCreate">if set to <c>true</c> the user is able to create a Trade.</param>
        public TradeTableViewModel(
            IEnumerable<Trade> model, int pageNumber, int pageSize, string userId, bool canCreate)
        {
            var tmp = model.ToArray().Select(trade => new TradeViewModel
            {
                TradeData = trade,
                CanOffer =
                    canCreate &&
                    userId != trade.Owner.Id &&
                    trade.Offers.All(o => o.Id != userId)
            });

            Trades = tmp.ToPagedList(pageNumber, pageSize);
        }

        public IPagedList<TradeViewModel> Trades { get; set; }
    }
}