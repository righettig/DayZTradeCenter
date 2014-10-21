namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public class AddedIsHardcoretoTrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "IsHardcore", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trades", "IsHardcore");
        }
    }
}
