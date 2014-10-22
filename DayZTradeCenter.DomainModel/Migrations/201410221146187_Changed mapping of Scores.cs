namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangedmappingofScores : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Scores_Id", "dbo.Scores");
            DropIndex("dbo.AspNetUsers", new[] { "Scores_Id" });
            
            //RenameColumn(table: "dbo.Scores", name: "Scores_Id", newName: "UserId");
            AddColumn("dbo.Scores", "UserId", c => c.String(nullable: false, maxLength: 128));

            DropPrimaryKey("dbo.Scores");
            AddPrimaryKey("dbo.Scores", "UserId");
            CreateIndex("dbo.Scores", "UserId");
            AddForeignKey("dbo.Scores", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Scores_Id");
            DropColumn("dbo.Scores", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scores", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.AspNetUsers", "Scores_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Scores", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Scores", new[] { "UserId" });
            DropPrimaryKey("dbo.Scores");
            AddPrimaryKey("dbo.Scores", "Id");
            RenameColumn(table: "dbo.Scores", name: "UserId", newName: "Scores_Id");
            CreateIndex("dbo.AspNetUsers", "Scores_Id");
            AddForeignKey("dbo.AspNetUsers", "Scores_Id", "dbo.Scores", "Id", cascadeDelete: true);
        }
    }
}
