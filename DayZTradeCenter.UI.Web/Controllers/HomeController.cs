using System;
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
    public class HomeController : Controller
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="tradeManager">The trade manager.</param>
        /// <exception cref="System.ArgumentNullException">tradeManager</exception>
        public HomeController(ITradeManager tradeManager)
        {
            if (tradeManager == null)
            {
                throw new ArgumentNullException("tradeManager");
            }

            _tradeManager = tradeManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #endregion

        #region Public properties

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
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Gets the sign in manager.
        /// </summary>
        /// <value>
        /// The sign in manager.
        /// </value>
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        #endregion

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var latestTrades = _tradeManager.GetLatestTrades();
            var hottestTrades = _tradeManager.GetHottestTrades();

            // DEBUG: no data
            //latestTrades = Enumerable.Empty<Trade>()

            if (!User.Identity.IsAuthenticated)
            {
                // DEBUG: fake data
                //var tmp = latestTrades
                //    .Concat(latestTrades)
                //    .Concat(latestTrades)
                //    .Concat(latestTrades)
                //    .Concat(latestTrades);

                return View("Landing", new LandingPageViewModel(latestTrades, hottestTrades));
            }
            
            var user = await GetCurrentUser();

            var vm = new DashboardViewModel(latestTrades, hottestTrades)
            {
                Reputation = user.GetReputation(),
                IsAdmin = UserManager.IsInRole(user.Id, "Administrator"),

                MyTrades = _tradeManager.GetTradesByUser(user.Id),
                MyOffers = _tradeManager.GetOffersByUser(user.Id)
            };

            return View(vm);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var user = await UserManager.FindByIdAsync(userId);

            return user;
        }

        #region Private fields

        private ApplicationUserManager _userManager;

        private ApplicationSignInManager _signInManager;

        private readonly ITradeManager _tradeManager;

        #endregion
    }
}