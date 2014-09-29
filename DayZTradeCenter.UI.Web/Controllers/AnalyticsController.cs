using System;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.UI.Web.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AnalyticsController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyticsController"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <param name="itemsRepository">The items repository.</param>
        /// <exception cref="System.ArgumentNullException">
        /// tradesRepository
        /// or
        /// itemsRepository
        /// </exception>
        public AnalyticsController(IRepository<Trade> tradesRepository, IRepository<Item> itemsRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            if (itemsRepository == null)
            {
                throw new ArgumentNullException("itemsRepository");
            }

            _tradesRepository = tradesRepository;
            _itemsRepository = itemsRepository;
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
                            Item = grp.Key.Item,
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
                            Item = grp.Key.Item,
                            Count = grp.Count()
                        });

            var items = _itemsRepository.GetAll();

            var vm = new AnalyticsViewModel
            {
                MostWantedItems = mostWantedItem,
                MostOfferedItems = mostOfferedItem,
                Items = new SelectList(items, "Id", "Name")
            };

            var chart = new Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories =
                        new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
                })
                .SetSeries(new Series
                {
                    Data =
                        new Data(new object[] {29.9, 71.5, 106.4, 129.2, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4})
                });

            vm.Chart = chart;

            return View(vm);
        }

        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;
        private readonly IRepository<Item> _itemsRepository;

        #endregion
    }
}