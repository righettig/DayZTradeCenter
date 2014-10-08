namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryInfotoItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Details_Category", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "Details_Subcategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Details_Subcategory");
            DropColumn("dbo.Items", "Details_Category");
        }
    }
}
