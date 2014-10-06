using System.Data.Entity;

namespace DayZTradeCenter.DomainModel
{
    public class DayZTradeCenterDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DayZTradeCenterDbContext"/> class.
        /// </summary>
        public DayZTradeCenterDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
