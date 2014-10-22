namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedScorestoApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bravery = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Scores_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Scores_Id");
            AddForeignKey("dbo.AspNetUsers", "Scores_Id", "dbo.Scores", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Scores_Id", "dbo.Scores");
            DropIndex("dbo.AspNetUsers", new[] { "Scores_Id" });
            DropColumn("dbo.AspNetUsers", "Scores_Id");
            DropTable("dbo.Scores");
        }
    }
}
