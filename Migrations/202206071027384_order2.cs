namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
        }
    }
}
