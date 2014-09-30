using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Identity.Services;
using DayZTradeCenter.UI.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class TradesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="tradeManager">The trade manager.</param>
        /// <exception cref="System.ArgumentNullException">tradeManager</exception>
        public TradesController(ITradeManager tradeManager)
        {
            if (tradeManager == null)
            {
                throw new ArgumentNullException("tradeManager");
            }

            _tradeManager = tradeManager;
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
            var model = _tradeManager.GetActiveTrades();

            var userId = User.Identity.GetUserId();

            var vm = new ListTradesViewModel(
                !User.IsInRole("Administrator"),
                userId,
                model,
                _tradeManager.CanCreateTrade(userId));

            return View(vm);
        }

        // GET: Trades/Create
        public ActionResult Create()
        {
            var items = _tradeManager.GetAllItems();

            ViewBag.Items =
                items.Select(item => new {item.Id, item.Name});

            return View();
        }

        // POST: Trades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(CreateTradeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    error = "It is not possible to create a trade for the same items."
                });
            }

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            _tradeManager.CreateNewTrade(vm.Have, vm.Want, user);

            return Json(new {success = true});
        }

        public ActionResult Delete(int tradeId)
        {
            var trade = _tradeManager.GetTradeById(tradeId);

            if (trade == null)
            {
                return HttpNotFound();
            }

            return trade.Owner.Id != User.Identity.GetUserId()
                ? View("Index")
                : View(trade);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int tradeId)
        {
            _tradeManager.DeleteTrade(tradeId, User.Identity.GetUserId());

            return RedirectToAction("Index");
        }

        // GET: Trades/Offer
        public async Task<ActionResult> Offer(int tradeId)
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);

            if (_tradeManager.Offer(tradeId, user))
            {
                return RedirectToAction("Index");
            }
            
            return View("AlreadyOffered");
        }

        // GET: Trades/Withdraw
        public ActionResult Withdraw(int tradeId)
        {
            var userId = User.Identity.GetUserId();
            
            _tradeManager.Withdraw(tradeId, userId);

            return RedirectToAction("Index");
        }

        // GET: Trades/Details/5
        public ActionResult Details(int id)
        {
            var model = _tradeManager.GetTradeById(id);

            return View(model);
        }

        // GET: Trades/ChooseWinner/tradeId=1&userId=2
        public ActionResult ChooseWinner(int tradeId, string userId)
        {
            _tradeManager.ChooseWinner(tradeId, userId);

            return RedirectToAction("Index", "Home");
        }

        // GET: Trades/ExchangeManagement/5
        public ActionResult ExchangeManagement(int id)
        {
            var model = new ExchangeManagementViewModel
            {
                Trade = _tradeManager.GetTradeById(id)
            };
            
            return View(model);
        }

        // POST: Trades/ExchangeManagement/5
        [HttpPost, ActionName("ExchangeManagement")]
        [ValidateAntiForgeryToken]
        public ActionResult ExchangeManagementConfirmed(ExchangeDetails details, Trade trade)
        {
            var message = new Message(
                string.Format("My SteamId is {0}. Meet me at {1}, server {2}, time {3} GTM",
                    details.SteamId,
                    details.Location,
                    details.Server,
                    details.Time));

            var model = new ExchangeManagementViewModel {Trade = trade};
            model.Messages.Add(message);

            return View("ExchangeManagement", model);
        }

        // POST: Trades/TradeCompleted/1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TradeCompleted(int id)
        {
            var trade = _tradeManager.GetTradeById(id);

            var user = await UserManager.FindByIdAsync(trade.Winner);
            
            var model = _tradeManager.MarkAsCompleted(id, user);

            await UserManager.UpdateAsync(user);

            return View(model);
        }

        [ActionName("TradeCompleted")]
        public ActionResult TradeCompletedGet(int id)
        {
            var model = _tradeManager.GetTradeById(id);
            
            return View(model);
        }

        public async Task<ActionResult> LeaveFeedback(int id, int score)
        {
            var trade = _tradeManager.GetTradeById(id);

            var user = await UserManager.FindByIdAsync(trade.Winner);
            
            _tradeManager.LeaveFeedback(id, score, user);

            await UserManager.UpdateAsync(user);

            return View();
        }

        private readonly ITradeManager _tradeManager;
    }
}