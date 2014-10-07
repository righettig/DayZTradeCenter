namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addedevents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        TimeStamp = c.DateTime(nullable: false),
                        Event = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)

                // added to create the FK relationship: EventInfoes -> AspNetUsers
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }
        
        public override void Down()
        {
            // added to drop the FK relationship: EventInfoes -> AspNetUsers
            DropForeignKey("dbo.EventInfoes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.EventInfoes", new[] { "UserId" });

            DropTable("dbo.EventInfoes");
        }
    }
}
