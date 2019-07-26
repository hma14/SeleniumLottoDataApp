namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_grand_from_DailyGrand : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DailyGrand", "Grand");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DailyGrand", "Grand", c => c.Int());
        }
    }
}
