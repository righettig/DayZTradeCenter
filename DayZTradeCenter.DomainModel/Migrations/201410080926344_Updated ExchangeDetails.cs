namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedExchangeDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExchangeDetails", "SteamId", c => c.String(nullable: false));
            AlterColumn("dbo.ExchangeDetails", "Location", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("dbo.ExchangeDetails", "Server", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExchangeDetails", "Server", c => c.String());
            AlterColumn("dbo.ExchangeDetails", "Location", c => c.String());
            AlterColumn("dbo.ExchangeDetails", "SteamId", c => c.String());
        }
    }
}
