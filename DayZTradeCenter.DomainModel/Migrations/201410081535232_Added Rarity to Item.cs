namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedRaritytoItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Rarity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Rarity");
        }
    }
}
