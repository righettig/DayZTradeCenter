using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Identity.Services;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.UI.Web.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Microsoft.AspNet.Identity;
using Events = DayZTradeCenter.DomainModel.Events;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class ProfileController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="profileManager">The profile manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <exception cref="ArgumentNullException">
        /// profileManager
        /// or
        /// userManager
        /// or
        /// signInManager
        /// </exception>
        public ProfileController(
            IProfileManager profileManager, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            if (profileManager == null)
            {
                throw new ArgumentNullException("profileManager");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (signInManager == null)
            {
                throw new ArgumentNullException("signInManager");
            }

            _profileManager = profileManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Edit

        public async Task<ActionResult> Edit()
        {
            var user = await GetCurrentUser();
            
            var vm = new ProfileViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                IsAdmin = _userManager.IsInRole(user.Id, "Administrator")
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(vm.Id);

                user.UserName = vm.Username;
                user.Email = vm.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    _signInManager.AuthenticationManager.SignOut();
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    _profileManager.AddHistoryEvent(user.Id, Events.ProfileUpdate);

                    return RedirectToAction("Edit");
                }
            }
            return View(vm);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        #endregion

        public async Task<ViewResult> Inbox()
        {
            var model =
                await _userManager.FindByIdAsync(
                    User.Identity.GetUserId());

            return View(model.Messages.OrderByDescending(m => m.Timestamp));
        }

        public ViewResult CompleteHistory()
        {
            var vm = new CompleteHistoryViewModel();

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

        private readonly IProfileManager _profileManager;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        #endregion
    }
}