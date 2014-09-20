using System.Security.Claims;
using System.Threading.Tasks;
using DayZTradeCenter.DomainModel.Identity.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace DayZTradeCenter.DomainModel.Identity.Services
{
    /// <summary>
    /// Configure the application sign-in manager which is used in this application.
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSignInManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="authenticationManager">The authentication manager.</param>
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
