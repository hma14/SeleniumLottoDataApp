namespace SeleniumLottoDataApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Column_TotalHits_Table_LottoNumbers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LottoNumbers", "TotalHits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LottoNumbers", "TotalHits");
        }
    }
}
