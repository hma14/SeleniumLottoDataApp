namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_DailyGrand_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyGrand",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.DailyGrand");
        }
    }
}
