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

        private readonly IRepository<Trade> _tradesRepository;
    }
}