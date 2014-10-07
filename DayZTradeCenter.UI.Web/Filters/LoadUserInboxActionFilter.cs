using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity;

namespace DayZTradeCenter.UI.Web.Filters
{
    /// <summary>
    /// Set the current user's inbox messages count on the Session object "user_inbox_count".
    /// </summary>
    public class LoadUserInboxActionFilter : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadUserInboxActionFilter"/> class.
        /// </summary>
        /// <param name="mgr">The MGR.</param>
        /// <exception cref="System.ArgumentNullException">mgr</exception>
        public LoadUserInboxActionFilter(ApplicationUserManager mgr)
        {
            if (mgr == null)
            {
                throw new ArgumentNullException("mgr");
            }

            _mgr = mgr;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext =
                filterContext.HttpContext;
            
            var userId = 
                httpContext.User.Identity.GetUserId();

            if (userId == null) // when the current user is not logged in.
            {
                return;
            }

            var user =
                _mgr.FindById(userId);
            
            Debug.Assert(
                httpContext.Session != null, "httpContext.Session != null");

            httpContext.Session["user_inbox_count"] = user.Messages.Count;

            if (user.Messages.Count > 0)
            {
                // takes the last 3 messages.
                httpContext.Session["user_inbox"] =
                    user.Messages.OrderByDescending(m => m.Timestamp).Take(3);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // do nothing
        }

        private readonly ApplicationUserManager _mgr;
    }
}