namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tables_LottoTypes_Numbers : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LottoTypes", t => t.LottoTypeId)
                .Index(t => t.LottoTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Numbers", "LottoTypeId", "dbo.LottoTypes");
            DropIndex("dbo.Numbers", new[] { "LottoTypeId" });
            DropTable("dbo.Numbers");
            DropTable("dbo.LottoTypes");
        }
    }
}
