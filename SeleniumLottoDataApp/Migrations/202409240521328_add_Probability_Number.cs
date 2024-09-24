namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Probability_Number : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Numbers", "Probability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Numbers", "Probability");
        }
    }
}
