using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Services;
using DayZTradeCenter.UI.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using rg.GenericRepository.Core;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class TradesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="tradesRepository">The trades repository.</param>
        /// <param name="itemsRepository">The items repository.</param>
        /// <exception cref="System.ArgumentNullException">
        /// tradesRepository
        /// or
        /// itemsRepository
        /// </exception>
        public TradesController(IRepository<Trade> tradesRepository, IRepository<Item> itemsRepository)
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

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        // GET: Trades
        public ActionResult Index()
        {
            var model = _tradesRepository.GetAll();

            var vm = new ListTradesViewModel(
                !User.IsInRole("Administrator"),
                User.Identity.GetUserId(),
                model);

            return View(vm);
        }

        // GET: Trades/Create
        public ActionResult Create()
        {
            var items = _itemsRepository.GetAll();

            return View(new CreateTradeViewModel
            {
                Items = new SelectList(items, "Id", "Name")
            });
        }

        // POST: Trades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HaveId, WantId")] CreateTradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var trade = new Trade();
            var have = _itemsRepository.GetSingle(vm.HaveId);
            trade.Have.Add(have);

            var want = _itemsRepository.GetSingle(vm.WantId);
            trade.Want.Add(want);

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            trade.Owner = user;

            // TODO: use TimeProvider
            trade.CreationDate = DateTime.Now;

            _tradesRepository.Insert(trade);
            _tradesRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Trades/Offer
        public async Task<ActionResult> Offer(int tradeId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);
            
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);

            trade.Offers.Add(user);

            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Trades/Withdraw
        public ActionResult Withdraw(int tradeId)
        {
            var trade = _tradesRepository.GetSingle(tradeId);

            var userId = User.Identity.GetUserId();
            var user = trade.Offers.FirstOrDefault(o => o.Id == userId);

            trade.Offers.Remove(user);
            
            _tradesRepository.Update(trade);
            _tradesRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        #region Private fields

        private readonly IRepository<Trade> _tradesRepository;
        private readonly IRepository<Item> _itemsRepository;

        #endregion
    }
}