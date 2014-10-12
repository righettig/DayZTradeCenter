using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Services;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="System.ArgumentNullException">userManager</exception>
        public AdminController(ApplicationUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<RedirectToRouteResult> Ban(string userId)
        {
            return await SetIsApproved(userId, false);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<RedirectToRouteResult> Unban(string userId)
        {
            return await SetIsApproved(userId, true);
        }

        private async Task<RedirectToRouteResult> SetIsApproved(string userId, bool isApproved)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.IsApproved = isApproved;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }

        private readonly ApplicationUserManager _userManager;
    }
}