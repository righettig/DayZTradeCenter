namespace DayZTradeCenter.DomainModel.Identity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedtimestamptomessages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Timestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Timestamp");
        }
    }
}
