using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Interfaces;
using DayZTradeCenter.DomainModel.Migrations;
using DayZTradeCenter.DomainModel.Services;
using DayZTradeCenter.UI.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DayZTradeCenter.UI.Web.Models;
using Events = DayZTradeCenter.DomainModel.Services.Events;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="profileManager">The profile manager.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="authenticationManager">The authentication manager.</param>
        /// <exception cref="ArgumentNullException">
        /// profileManager
        /// or
        /// userManager
        /// or
        /// signInManager
        /// or
        /// authenticationManager
        /// </exception>
        public AccountController(
            IProfileManager profileManager, 
            ApplicationUserManager userManager, 
            ApplicationSignInManager signInManager, 
            IAuthenticationManager authenticationManager)
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

            if (authenticationManager == null)
            {
                throw new ArgumentNullException("authenticationManager");
            }

            _profileManager = profileManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        #region Login / Logoff

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
#if FAKE_LOGIN
            return View("LoginWithFakeAccount");
#else
            return ExternalLogin(returnUrl);
#endif
        }

        //
        // POST: /Account/AdminLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminLogin(string username, string password, string returnUrl)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result =
                await _signInManager.PasswordSignInAsync(username, password, false, shouldLockout: false);

            if (result == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }
        
        #endregion

        #region ExternalLogin

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult("Steam",
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
#if FAKE_LOGIN
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string userId)
#else
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
#endif
        {
#if FAKE_LOGIN
            var testUser = DefaultUsers.All.First(user => user.UserId == userId);

            var loginInfo = new ExternalLoginInfo
            {
                DefaultUserName = testUser.UserName,
                Login = new UserLoginInfo("Steam", "http://steamcommunity.com/openid/id/" + testUser.SteamId),
                ExternalIdentity = new ClaimsIdentity("ExternalCookie")
            };
#else
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var accountInfo =
                getSteamIdAndUserName(loginInfo.ExternalIdentity.Claims.ToArray());

            var steamId = long.Parse(accountInfo.Item1);
            if (steamId != 76561198064455333 && !SteamHelper.DoIHaveDayZ(steamId))
            {
                return View("Unauthorized");
            }
#endif
            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Username = loginInfo.DefaultUserName});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _authenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser {UserName = model.Username, Email = model.Email};
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        _profileManager.AddHistoryEvent(user.Id, Events.Registration);

                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private static Tuple<string, string> getSteamIdAndUserName(Claim[] claims)
        {
            var id =
                Path.GetFileName(
                    claims.First(c => c.Type.EndsWith("nameidentifier")).Value);

            var userName = claims.First(c => c.Type.EndsWith("name")).Value;

            return new Tuple<string, string>(id, userName);
        }

        #endregion

        #region Private fields
        
        private readonly IProfileManager _profileManager;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        #endregion
    }
}