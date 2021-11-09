namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_Database_Lottotrydb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cash4Life", "DrawDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cash4Life_CashBall", "DrawDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lotto649", "DrawNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.LottoMax", "DrawDate", c => c.DateTime(nullable: false));
         
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.SuperLotto",
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
                "dbo.SuperLotto_Rear",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        RearNumber1 = c.Int(),
                        RearNumber2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.SSQ",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.SSQ_Blue",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Blue = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.SevenLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Number7 = c.Int(),
                        Special = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.PowerBall",
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
                "dbo.PowerBall_PowerBall",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        PowerBall = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.OZLottoWed",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Supp1 = c.Int(),
                        Supp2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.OZLottoTue",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Number7 = c.Int(),
                        Supp1 = c.Int(),
                        Supp2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.OZLottoSat",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Supp1 = c.Int(),
                        Supp2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.OZLottoMon",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Supp1 = c.Int(),
                        Supp2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.OregonMegabucks",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.NYSweetMillion",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.NYLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Bonus = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
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
                "dbo.NewJerseyPick6Lotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.MyProduct",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MegaMillions_MegaBall",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        MegaBall = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.MegaMillions",
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
                "dbo.Lottos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100),
                        links = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.LottoName",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GermanLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Bonus = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.FloridaLucky",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Lb = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.FloridaLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Xtra = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.FloridaFantasy5",
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
                "dbo.EuroMillions_LuckyStars",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Star1 = c.Int(),
                        Star2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.EuroMillions",
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
                "dbo.EuroJackpot",
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
                "dbo.EuroJackpot_Euros",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Euro1 = c.Int(),
                        Euro2 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.DailyGrand_GrandNumber",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25),
                        GrandNumber = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
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
            
            CreateTable(
                "dbo.ConnecticutLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.ColoradoLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.CaSuperlottoPlus_Mega",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Mega = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            CreateTable(
                "dbo.CaSuperlottoPlus",
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
                "dbo.BritishLotto",
                c => new
                    {
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.String(maxLength: 25, unicode: false),
                        Number1 = c.Int(),
                        Number2 = c.Int(),
                        Number3 = c.Int(),
                        Number4 = c.Int(),
                        Number5 = c.Int(),
                        Number6 = c.Int(),
                        Bonus = c.Int(),
                    })
                .PrimaryKey(t => t.DrawNumber);
            
            AlterColumn("dbo.LottoMax", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.Lotto649", "DrawNumber", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Cash4Life_CashBall", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.Cash4Life", "DrawDate", c => c.String(maxLength: 25));
        }
    }
}
