namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Lottery", newName: "Lotto649");
            CreateTable(
                "dbo.LottoNumbers",
                c => new
                    {
                        LottoName = c.Int(nullable: false),
                        DrawNumber = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        DrawDate = c.DateTime(nullable: false),
                        NumberRange = c.Int(nullable: false),
                        Distance = c.Int(nullable: false),
                        IsHit = c.Boolean(nullable: false),
                        NumberofDrawsWhenHit = c.Int(nullable: false),
                        IsBonusNumber = c.Boolean(nullable: false),
                        TotalHits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LottoName, t.DrawNumber, t.Number });
            
            CreateTable(
                "dbo.LottoTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LottoName = c.Int(nullable: false),
                        DrawNumber = c.Int(nullable: false),
                        DrawDate = c.DateTime(nullable: false),
                        NumberRange = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Numbers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.Int(nullable: false),
                        LottoTypeId = c.Guid(),
                        Distance = c.Int(nullable: false),
                        IsHit = c.Boolean(nullable: false),
                        NumberofDrawsWhenHit = c.Int(nullable: false),
                        IsBonusNumber = c.Boolean(nullable: false),
                        TotalHits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LottoTypes", t => t.LottoTypeId)
                .Index(t => t.LottoTypeId);
            
            AlterColumn("dbo.BC49", "DrawDate", c => c.String(nullable: false));
            AlterColumn("dbo.BritishLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.Cash4Life", "DrawDate", c => c.String(nullable: false));
            AlterColumn("dbo.Cash4Life_CashBall", "DrawDate", c => c.String(nullable: false));
            AlterColumn("dbo.CaSuperlottoPlus", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.CaSuperlottoPlus_Mega", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.ColoradoLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.ConnecticutLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.EuroJackpot_Euros", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.EuroJackpot", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.EuroMillions", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.EuroMillions_LuckyStars", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.FloridaFantasy5", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.FloridaLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.FloridaLucky", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.GermanLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.Lotto649", "DrawDate", c => c.String(nullable: false));
            AlterColumn("dbo.LottoMax", "DrawDate", c => c.String(nullable: false));
            AlterColumn("dbo.MegaMillions", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.MegaMillions_MegaBall", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.NewJerseyPick6Lotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.NewYorkTake5", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.NYLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.NYSweetMillion", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.OregonMegabucks", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.OZLottoMon", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.OZLottoSat", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.OZLottoTue", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.OZLottoWed", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.PowerBall_PowerBall", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.PowerBall", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.SevenLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.SSQ_Blue", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.SSQ", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.SuperLotto_Rear", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.SuperLotto", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.TexasCashFive", "DrawDate", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Numbers", "LottoTypeId", "dbo.LottoTypes");
            DropIndex("dbo.Numbers", new[] { "LottoTypeId" });
            AlterColumn("dbo.TexasCashFive", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.SuperLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.SuperLotto_Rear", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.SSQ", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.SSQ_Blue", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.SevenLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.PowerBall", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.PowerBall_PowerBall", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.OZLottoWed", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.OZLottoTue", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.OZLottoSat", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.OZLottoMon", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.OregonMegabucks", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.NYSweetMillion", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.NYLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.NewYorkTake5", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.NewJerseyPick6Lotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.MegaMillions_MegaBall", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.MegaMillions", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.LottoMax", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.Lotto649", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.GermanLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.FloridaLucky", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.FloridaLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.FloridaFantasy5", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.EuroMillions_LuckyStars", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.EuroMillions", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.EuroJackpot", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.EuroJackpot_Euros", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.ConnecticutLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.ColoradoLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.CaSuperlottoPlus_Mega", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.CaSuperlottoPlus", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.Cash4Life_CashBall", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.Cash4Life", "DrawDate", c => c.String(maxLength: 25));
            AlterColumn("dbo.BritishLotto", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            AlterColumn("dbo.BC49", "DrawDate", c => c.String(maxLength: 25, unicode: false));
            DropTable("dbo.Numbers");
            DropTable("dbo.LottoTypes");
            DropTable("dbo.LottoNumbers");
            RenameTable(name: "dbo.Lotto649", newName: "Lottery");
        }
    }
}
