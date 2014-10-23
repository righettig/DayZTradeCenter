namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedinterests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ItemId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interests", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Interests", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Interests", new[] { "ItemId" });
            DropIndex("dbo.Interests", new[] { "UserId" });
            DropTable("dbo.Interests");
        }
    }
}
