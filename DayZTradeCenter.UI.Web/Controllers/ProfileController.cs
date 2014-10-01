using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel;
using DayZTradeCenter.DomainModel.Identity.Entities;
using DayZTradeCenter.DomainModel.Identity.Services;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.UI.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class ProfileController : Controller
    {
        #region Ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="profileManager">The profile manager.</param>
        /// <exception cref="System.ArgumentNullException">profileManager</exception>
        public ProfileController(IProfileManager profileManager)
        {
            if (profileManager == null)
            {
                throw new ArgumentNullException("profileManager");
            }

            _profileManager = profileManager;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        public ProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        #region Edit

        public async Task<ActionResult> Edit()
        {
            var user = await GetCurrentUser();
            
            var vm = new ProfileViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                IsAdmin = UserManager.IsInRole(user.Id, "Administrator")
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(vm.Id);

                user.UserName = vm.Username;
                user.Email = vm.Email;

                var result = await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    SignInManager.AuthenticationManager.SignOut();
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    _profileManager.AddHistoryEvent(user.Id, Events.ProfileUpdate);

                    return RedirectToAction("Edit");
                }
            }
            return View(vm);
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();

            var user = await UserManager.FindByIdAsync(userId);

            return user;
        }

        #endregion

        public async Task<ViewResult> Inbox()
        {
            var model =
                await UserManager.FindByIdAsync(
                    User.Identity.GetUserId());

            return View(model.Messages);
        }

        #region Private fields

        private ApplicationUserManager _userManager;

        private ApplicationSignInManager _signInManager;

        private readonly IProfileManager _profileManager;

        #endregion
    }
}