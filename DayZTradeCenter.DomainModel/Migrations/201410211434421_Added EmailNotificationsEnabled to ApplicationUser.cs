namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmailNotificationsEnabledtoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmailNotificationsEnabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EmailNotificationsEnabled");
        }
    }
}
