namespace DayZTradeCenter.DomainModel.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedConditiontoTradeDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TradeDetails", "Condition", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TradeDetails", "Condition");
        }
    }
}
