namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "CampaingId", c => c.Int());
            AlterColumn("dbo.Categories", "MainCategoryId", c => c.Int());
            AlterColumn("dbo.Categories", "CompaignId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CompaignId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "MainCategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "CampaingId", c => c.Int(nullable: false));
        }
    }
}
