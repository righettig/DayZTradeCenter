namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsApprovedtoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsApproved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsApproved");
        }
    }
}
