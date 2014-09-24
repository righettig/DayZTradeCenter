using System;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.UI.Web.Models;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsController"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <exception cref="System.ArgumentNullException">tradesRepository</exception>
        public AnalyticsController(IRepository<Trade> tradesRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            _tradesRepository = tradesRepository;
        }

        // GET: Analytics
        public ActionResult Index()
        {
            var mostWantedItem =
                _tradesRepository
                    .GetAll()
                    .GroupBy(trade => trade.Want.First()) // NB: I'm assuming only a single item here.
                    .OrderByDescending(grp => grp.Count())
                    .Select(
                        grp => new AnalyticsViewModel.ItemDetails
                        {
                            Item = grp.Key,
                            Count = grp.Count()
                        });

            var mostOfferedItem =
                _tradesRepository
                    .GetAll()
                    .GroupBy(trade => trade.Have.First()) // NB: I'm assuming only a single item here.
                    .OrderByDescending(grp => grp.Count())
                    .Select(
                        grp => new AnalyticsViewModel.ItemDetails
                        {
                            Item = grp.Key,
                            Count = grp.Count()
                        });

            var vm = new AnalyticsViewModel
            {
                MostWantedItems = mostWantedItem,
                MostOfferedItems = mostOfferedItem
            };

            return View(vm);
        }

        private readonly IRepository<Trade> _tradesRepository;
    }
}