using DayZTradeCenter.DomainModel.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel.Identity
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class.
    // Please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}