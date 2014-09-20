using DayZTradeCenter.DomainModel.Identity.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace DayZTradeCenter.DomainModel.Identity.Services
{
    /// <summary>
    /// Configure the application user manager used in this application.
    /// UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = 
                new ApplicationUserManager(
                    new UserStore<ApplicationUser>(
                        context.Get<ApplicationDbContext>()));
            
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            return manager;
        }
    }
}