using System;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity;

namespace DayZTradeCenter.UI.Web.Controllers
{
    public class PeopleController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <exception cref="System.ArgumentNullException">userManager</exception>
        public PeopleController(ApplicationUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }
            
            _userManager = userManager;
        }

        // GET: People
        public ActionResult Index()
        {
            var users = _userManager
                .Users.ToArray() // otherwise Linq to Entities is unable to translate "IsInRole"
                .Where(
                    u => !_userManager.IsInRole(u.Id, "Administrator"))
                .Where(u => u.Scores.Bravery > 0)
                .OrderByDescending(u => u.Scores.Bravery);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PeopleTable", users);
            }

            return View(users);
        }

        private readonly ApplicationUserManager _userManager;
    }
}