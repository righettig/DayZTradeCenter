using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.UI.Web.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Microsoft.AspNet.Identity;
using Events = DayZTradeCenter.DomainModel.Services.Events;

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
                EmailNotificationsEnabled = user.EmailNotificationsEnabled,
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
                user.EmailNotificationsEnabled = vm.EmailNotificationsEnabled;

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

        [HttpPost]
        public async Task<JsonResult> DeleteInboxMessages(params int[] ids)
        {
            var user =
                await _userManager.FindByIdAsync(
                    User.Identity.GetUserId());

            var messages =
                user.Messages;

            foreach (var message in ids.Select(
                id => messages.FirstOrDefault(m => m.Id == id)).Where(message => message != null))
            {
                messages.Remove(message);
            }

            await _userManager.UpdateAsync(user);

            return Json(new {success = true});
        }

        public async Task<ViewResult> CompleteHistory(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var data = _profileManager.GetReputationHistory(user).ToArray();

            var vm = new CompleteHistoryViewModel();

            var chart = new Highcharts("chart")
                .SetXAxis(new XAxis
                {
                    Categories =
                        data.Select(x => x.Date.ToShortDateString()).ToArray()
                })
                .SetSeries(new Series
                {
                    Data =
                        new Data(data.Select(x => x.Value).Cast<object>().ToArray())
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