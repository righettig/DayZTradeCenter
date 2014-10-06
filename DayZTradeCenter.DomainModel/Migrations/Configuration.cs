namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DayZTradeCenterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DayZTradeCenterDbContext context)
        {
            context.Items.AddOrUpdate(item => item.Id,
                new Item {Id = 0, Name = "Mosin"},
                new Item {Id = 1, Name = "SKS"},
                new Item {Id = 2, Name = "Tent"},
                new Item {Id = 3, Name = "Pitchfork"},
                new Item {Id = 4, Name = "Crowbar"});
        }
    }
}
