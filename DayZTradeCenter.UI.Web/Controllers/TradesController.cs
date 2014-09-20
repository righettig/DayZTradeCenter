using System;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    [Authorize]
    public class TradesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <exception cref="System.ArgumentNullException">tradesRepository</exception>
        public TradesController(IRepository<Trade> tradesRepository)
        {
            if (tradesRepository == null)
            {
                throw new ArgumentNullException("tradesRepository");
            }

            _tradesRepository = tradesRepository;
        }

        // GET: Trades
        public ActionResult Index()
        {
            var model = _tradesRepository.GetAll();

            return View(model);
        }

        private readonly IRepository<Trade> _tradesRepository;
    }
}