using System;
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
        /// <param name="trackedItemIds">The tracked item ids.</param>
        /// <exception cref="System.ArgumentNullException">
        /// model
        /// or
        /// trackedItemIds
        /// </exception>
        public TradeTableViewModel(
            IEnumerable<Trade> model, int pageNumber, int pageSize, string userId, bool canCreate,
            IEnumerable<int> trackedItemIds)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            
            if (trackedItemIds == null)
            {
                throw new ArgumentNullException("trackedItemIds");
            }
            
            var tmp = model.ToArray().Select(trade => new TradeViewModel
            {
                Id = trade.Id,
                OwnerId = trade.Owner.Id,
                OwnerReputation = trade.Owner.GetReputation(),
                OffersCount = trade.Offers.Count,
                IsExperimental = trade.IsExperimental,
                IsHardcore = trade.IsHardcore,

                Have = trade.Have.Select(x => new TradeDetailsViewModel(x, trackedItemIds)),
                Want = trade.Want.Select(x => new TradeDetailsViewModel(x, trackedItemIds)),

                CanOffer =
                    canCreate &&
                    userId != trade.Owner.Id &&
                    trade.Offers.All(o => o.Id != userId)
            });

            Trades = tmp.ToPagedList(pageNumber, pageSize);
        }

        public bool IsAdmin { get; set; }

        public IPagedList<TradeViewModel> Trades { get; set; }

        public int TotalItemCount
        {
            get { return Trades.TotalItemCount; }
        }
    }
}