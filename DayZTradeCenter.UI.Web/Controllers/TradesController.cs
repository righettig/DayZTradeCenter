using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities.Messages;
using DayZTradeCenter.DomainModel.Identity.Services;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.UI.Web.Models;
using Microsoft.AspNet.Identity;
using ItemViewModel = DayZTradeCenter.UI.Web.Models.ItemViewModel;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class TradesController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradesController"/> class.
        /// </summary>
        /// <param name="tradeManager">The trade manager.</param>
        /// <param name="profileManager">The profile manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="ArgumentNullException">
        /// tradeManager
        /// or
        /// profileManager
        /// or
        /// userManager
        /// </exception>
        public TradesController(
            ITradeManager tradeManager, IProfileManager profileManager, ApplicationUserManager userManager)
        {
            if (tradeManager == null)
            {
                throw new ArgumentNullException("tradeManager");
            }

            if (profileManager == null)
            {
                throw new ArgumentNullException("profileManager");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            _tradeManager = tradeManager;
            _profileManager = profileManager;
            _userManager = userManager;
        }

        // GET: Trades
        public ActionResult Index(int? itemId, SearchTypes? searchType)
        {
            var model = _tradeManager.GetActiveTrades(
                new SearchParams {ItemId = itemId, Type = searchType});

            var userId = User.Identity.GetUserId();

            var vm = new ListTradesViewModel(
                !User.IsInRole("Administrator"),
                userId,
                model,
                _tradeManager.CanCreateTrade(userId),
                searchType.HasValue);

            var items = _tradeManager.GetAllItems();

            vm.Items =
                items.Select(item => new ItemViewModel {Id = item.Id, Name = item.Name});

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

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            _tradeManager.CreateNewTrade(vm.Have, vm.Want, user);
            _profileManager.AddHistoryEvent(user.Id, Events.TradeCreated);

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
            var userId = User.Identity.GetUserId();

            _tradeManager.DeleteTrade(tradeId, userId);
            _profileManager.AddHistoryEvent(userId, Events.TradeDeleted);

            return RedirectToAction("Index");
        }

        // GET: Trades/Offer
        public async Task<ActionResult> Offer(int tradeId)
        {
            var userId = User.Identity.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            switch (_tradeManager.Offer(tradeId, user))
            {
                case OfferResult.Success:
                    _profileManager.AddHistoryEvent(userId, Events.TradeOffered);
                    return RedirectToAction("Index");

                case OfferResult.AlreadOffered:
                    return View("AlreadyOffered");

                case OfferResult.OwnerCannotOffer:
                    return RedirectToAction("Index", "Home");

                default:
                    throw new NotSupportedException();
            }
        }

        // GET: Trades/Withdraw
        public ActionResult Withdraw(int tradeId)
        {
            var userId = User.Identity.GetUserId();
            
            if (_tradeManager.Withdraw(tradeId, userId))
            {
                _profileManager.AddHistoryEvent(userId, Events.TradeWithdrawn);
            }
        
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
            if (_tradeManager.ChooseWinner(tradeId, userId))
            {
                _profileManager.AddHistoryEvent(User.Identity.GetUserId(), Events.WinnerChoosen);
            }

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
        public async Task<ActionResult> ExchangeManagementConfirmed(ExchangeDetails details, int tradeId)
        {
            var trade = _tradeManager.GetTradeById(tradeId);

            var message = new ExchangeDetailsMessage(details);

            var model = new ExchangeManagementViewModel {Trade = trade};
            model.Messages.Add(message);

            var winner = await _userManager.FindByIdAsync(trade.Winner);
            winner.Messages.Add(message);

            await _userManager.UpdateAsync(winner);

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

            var user = await _userManager.FindByIdAsync(trade.Winner);
            
            var model = _tradeManager.MarkAsCompleted(id, user);
            _profileManager.AddHistoryEvent(User.Identity.GetUserId(), Events.TradeCompleted);

            await _userManager.UpdateAsync(user);

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

            var user = await _userManager.FindByIdAsync(trade.Winner);

            if (_tradeManager.LeaveFeedback(id, score, user))
            {
                _profileManager.AddHistoryEvent(User.Identity.GetUserId(), Events.FeedbackLeft);
            }

            await _userManager.UpdateAsync(user);

            return View();
        }

        #region Private fields

        private readonly ITradeManager _tradeManager;
        private readonly IProfileManager _profileManager;
        private readonly ApplicationUserManager _userManager;

        #endregion
    }
}