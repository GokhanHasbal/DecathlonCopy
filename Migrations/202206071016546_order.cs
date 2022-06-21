namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.Orders", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "CargoId" });
            DropIndex("dbo.Orders", new[] { "PermissionId" });
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Orders", "Total", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "AddressHeader", c => c.String());
            AddColumn("dbo.Orders", "MyAddress", c => c.String());
            AddColumn("dbo.Orders", "City", c => c.String());
            AddColumn("dbo.Orders", "Notes", c => c.String());
            DropColumn("dbo.Orders", "piece");
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "AddressId");
            DropColumn("dbo.Orders", "CargoId");
            DropColumn("dbo.Orders", "PermissionId");
            DropColumn("dbo.Orders", "View");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "View", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "PermissionId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CargoId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "AddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "piece", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderLines", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderLines", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderLines", new[] { "ProductId" });
            DropIndex("dbo.OrderLines", new[] { "OrderId" });
            DropColumn("dbo.Orders", "Notes");
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "MyAddress");
            DropColumn("dbo.Orders", "AddressHeader");
            DropColumn("dbo.Orders", "Total");
            DropTable("dbo.OrderLines");
            CreateIndex("dbo.Orders", "PermissionId");
            CreateIndex("dbo.Orders", "CargoId");
            CreateIndex("dbo.Orders", "AddressId");
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Orders", "PermissionId", "dbo.Permissions", "Id");
            AddForeignKey("dbo.Orders", "CargoId", "dbo.Cargoes", "Id");
            AddForeignKey("dbo.Orders", "AddressId", "dbo.Addresses", "Id");
        }
    }
}
