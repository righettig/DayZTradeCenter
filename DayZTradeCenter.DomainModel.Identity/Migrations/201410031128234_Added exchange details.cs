namespace DayZTradeCenter.DomainModel.Identity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedexchangedetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SteamId = c.String(),
                        Location = c.String(),
                        Server = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Messages", "Details_Id", c => c.Int());
            CreateIndex("dbo.Messages", "Details_Id");
            AddForeignKey("dbo.Messages", "Details_Id", "dbo.ExchangeDetails", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Details_Id", "dbo.ExchangeDetails");
            DropIndex("dbo.Messages", new[] { "Details_Id" });
            DropColumn("dbo.Messages", "Details_Id");
            DropTable("dbo.ExchangeDetails");
        }
    }
}
