using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.UI.Web.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
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
        public HomeController(
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

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var latestTrades = _tradeManager.GetLatestTrades();
            var hottestTrades = _tradeManager.GetHottestTrades();

            if (!User.Identity.IsAuthenticated)
            {
                return View("Landing", new LandingPageViewModel(latestTrades, hottestTrades));
            }
            
            var user = await GetCurrentUser();

            if (_userManager.IsInRole(user.Id, "Administrator"))
            {
                return View("AdminIndex", new AdminDashboardViewModel
                {
                    Users = _userManager.Users
                        .ToArray() // NB: IsInRole, GetReputation cannot be translated in a Linq-To-Entities query.
                        .Where(
                            u => !_userManager.IsInRole(u.Id, "Administrator"))
                        .OrderByDescending(u => u.GetReputation())
                });
            }

            var vm = new DashboardViewModel(
                latestTrades, hottestTrades, user.Id, _tradeManager.GetOffersByUser(user.Id))
            {
                MyTrades = _tradeManager.GetTradesByUser(user.Id),

                History = _profileManager.GetHistoryByUserId(user.Id),

                Stats = GetRankingDetails(user)
            };

            return View(vm);
        }

        [AllowAnonymous]
        public PartialViewResult GetGallery()
        {
            var latestTrades = _tradeManager.GetLatestTrades();
            var hottestTrades = _tradeManager.GetHottestTrades();

            return PartialView("_TradeGallery", new LandingPageViewModel(latestTrades, hottestTrades));
        }

        [AllowAnonymous]
        public ViewResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ViewResult WebApi()
        {
            return View();
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        private UserStats GetRankingDetails(ApplicationUser user)
        {
            var userId = user.Id;
            var reputation = user.GetReputation();

            var allReputations = _userManager.Users
                .ToArray()
                .Where(u => !_userManager.GetRoles(u.Id).Contains("Administrator"))
                .Select(x => new {UserId = x.Id, Reputation = x.GetReputation()})
                .OrderByDescending(x => x.Reputation)
                .Select((x, i) => new {Index = i, x.UserId, x.Reputation})
                .ToArray();

            float? targetReputation = null;

            allReputations
                .TakeWhile(x => x.UserId != userId && Math.Abs(x.Reputation - reputation) > 0.001f)
                .LastOrDefault()
                .IfNotNull(x => targetReputation = x.Reputation);

            return new UserStats
            {
                Ranking = allReputations.First(x => x.UserId == userId).Index + 1,
                Reputation = reputation > 0 ? reputation : (float?) null,
                TargetReputation = targetReputation
            };
        }

        #region Private fields

        private readonly ITradeManager _tradeManager;
        private readonly IProfileManager _profileManager;
        private readonly ApplicationUserManager _userManager;

        #endregion
    }
}