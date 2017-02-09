namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewYorkTake5",
                c => new
                {
                    DrawNumber = c.Int(nullable: false),
                    DrawDate = c.String(maxLength: 25, unicode: false),
                    Number1 = c.Int(),
                    Number2 = c.Int(),
                    Number3 = c.Int(),
                    Number4 = c.Int(),
                    Number5 = c.Int(),
                })
                .PrimaryKey(t => t.DrawNumber);

            CreateTable(
                "dbo.TexasCashFive",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
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
            DropTable("dbo.TexasCashFive");            
            DropTable("dbo.NewYorkTake5");
            
        }
    }
}
