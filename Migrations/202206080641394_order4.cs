namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.String());
            AddColumn("dbo.Orders", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "View", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Delete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Delete");
            DropColumn("dbo.Orders", "View");
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
