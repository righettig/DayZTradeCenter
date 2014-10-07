using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.UI.Web.Models;
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
                return View("AdminIndex");
            }

            var reputation = user.GetReputation();
            var vm = new DashboardViewModel(latestTrades, hottestTrades)
            {
                Reputation = reputation > 0 ? reputation : (float?) null,
                
                MyTrades = _tradeManager.GetTradesByUser(user.Id),
                MyOffers = _tradeManager.GetOffersByUser(user.Id),

                History = _profileManager.GetHistoryByUserId(user.Id)
            };

            return View(vm);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        #region Private fields

        private readonly ITradeManager _tradeManager;
        private readonly IProfileManager _profileManager;
        private readonly ApplicationUserManager _userManager;

        #endregion
    }
}