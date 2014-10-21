namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsExperimentaltoTrade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trades", "IsExperimental", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trades", "IsExperimental");
        }
    }
}
