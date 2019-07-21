namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_DailyGrand_table : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.DailGrand", newName: "DailyGrand");
            //DropTable("dbo.DailGrand");
        }
        
        public override void Down()
        {
            //RenameTable(name: "dbo.DailyGrand", newName: "DailGrand");
        }
    }
}
