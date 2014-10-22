using System.Data.Entity;
using DayZTradeCenter.DomainModel.Entities;
using DayZTradeCenter.DomainModel.Migrations;
using DayZTradeCenter.DomainModel.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DayZTradeCenter.DomainModel
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

        public DbSet<Item> Items { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<EventInfo> Events { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

#if !DEBUG
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
#endif

            modelBuilder.Entity<TradeDetails>()
                .HasRequired(td => td.Item);

            modelBuilder.Entity<Trade>()
                .HasMany(t => t.Have);

            modelBuilder.Entity<Trade>()
                .HasMany(t => t.Want);

            modelBuilder.Entity<Trade>()
                .HasMany(t => t.Offers)
                .WithMany() // <- no parameter here because there is no navigation property
                .Map(m =>
                {
                    m.MapLeftKey("TradeId");
                    m.MapRightKey("UserId");
                    m.ToTable("Offers");
                });

            modelBuilder.Entity<Scores>()
                .HasKey(x => x.UserId)
                .HasRequired(x => x.User);
        }
    }
}