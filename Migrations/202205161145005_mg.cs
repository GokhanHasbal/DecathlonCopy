namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Campaing_Id", "dbo.Campaings");
            DropForeignKey("dbo.Products", "CampaingId", "dbo.Campaings");
            DropIndex("dbo.Products", new[] { "CampaingId" });
            DropIndex("dbo.Categories", new[] { "Campaing_Id" });
            DropColumn("dbo.Categories", "Campaing_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Campaing_Id", c => c.Int());
            CreateIndex("dbo.Categories", "Campaing_Id");
            CreateIndex("dbo.Products", "CampaingId");
            AddForeignKey("dbo.Products", "CampaingId", "dbo.Campaings", "Id");
            AddForeignKey("dbo.Categories", "Campaing_Id", "dbo.Campaings", "Id");
        }
    }
}
