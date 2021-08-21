namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Cash4Life : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cash4Life_CashBall",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25),
                        CashBall = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            DropColumn("dbo.Cash4Life", "CashBall");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cash4Life", "CashBall", c => c.Int());
            DropTable("dbo.Cash4Life_CashBall");
        }
    }
}
