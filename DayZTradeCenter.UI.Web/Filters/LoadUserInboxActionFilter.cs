using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using DayZTradeCenter.DomainModel.Identity.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DayZTradeCenter.UI.Web.Filters
{
    /// <summary>
    /// Set the current user's inbox messages count on the Session object "user_inbox".
    /// </summary>
    public class LoadUserInboxActionFilter : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = 
                filterContext.HttpContext;
            
            var mgr = 
                httpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var userId = 
                httpContext.User.Identity.GetUserId();

            if (userId == null) // when the current user is not logged in.
            {
                return;
            }
            
            var user =
                mgr.FindById(userId);

            Debug.Assert(
                httpContext.Session != null, "httpContext.Session != null");

            httpContext.Session["user_inbox"] = user.Messages.Count;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // do nothing
        }
    }
}