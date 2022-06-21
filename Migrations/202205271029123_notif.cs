namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "View", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "View", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "View");
            DropColumn("dbo.Orders", "View");
        }
    }
}
