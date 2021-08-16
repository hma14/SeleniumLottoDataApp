namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Cash4Life_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cash4Life",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        CashBall = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cash4Life");
        }
    }
}
