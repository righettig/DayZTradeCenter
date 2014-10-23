using System.Linq;
using System.Threading.Tasks;
using DayZTradeCenter.DomainModel.Entities;
using Microsoft.AspNet.Identity;

namespace DayZTradeCenter.DomainModel.Services
{
    public interface IUserManager
    {
        Task SendEmailAsync(string userId, string subject, string body);
        IQueryable<ApplicationUser> Users { get; }
    }

    /// <summary>
    /// Configure the application user manager used in this application.
    /// UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    /// <remarks>
    /// No email service is defined.
    /// </remarks>
    public class ApplicationUserManagerNoEmailService : UserManager<ApplicationUser>, IUserManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="store">The store.</param>
        public ApplicationUserManagerNoEmailService(IUserStore<ApplicationUser> store)
            : base(store)
        {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            user.IsApproved = true;
            user.Scores = new Scores();

            return base.CreateAsync(user);
        }
    }

    /// <summary>
    /// Configure the application user manager used in this application.
    /// UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class ApplicationUserManager : ApplicationUserManagerNoEmailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="emailService">The email service.</param>
        /// <exception cref="System.ArgumentNullException">emailService</exception>
        public ApplicationUserManager(IUserStore<ApplicationUser> store, IIdentityMessageService emailService)
            : base(store)
        {
            EmailService = emailService;
        }
    }
}