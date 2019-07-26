namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_new_table_DailyGrand_GrandNumber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyGrand_GrandNumber",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25),
                        GrandNumber = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyGrand_GrandNumber");
        }
    }
}
