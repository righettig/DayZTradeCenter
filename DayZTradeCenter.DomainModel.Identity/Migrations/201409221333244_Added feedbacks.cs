namespace DayZTradeCenter.DomainModel.Identity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedfeedbacks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        TradeId = c.Int(nullable: false),
                        From = c.String(),
                        Score = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Feedbacks", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Feedbacks");
        }
    }
}
