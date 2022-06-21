namespace Decathlon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Country = c.String(),
                        Couty = c.String(),
                        District = c.String(),
                        Street = c.String(),
                        PostCode = c.String(),
                        BuildingNo = c.String(),
                        ApartmentNo = c.String(),
                        OpenAddress = c.String(),
                        Status = c.Boolean(nullable: false),
                        Selection = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        piece = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        CargoId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Cargoes", t => t.CargoId)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.AddressId)
                .Index(t => t.CargoId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Logo = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        TcNo = c.String(),
                        Phone = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Code = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        GradeScore = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        GradeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grades", t => t.GradeId)
                .ForeignKey("dbo.Permissions", t => t.PermissionId)
                .Index(t => t.PermissionId)
                .Index(t => t.GradeId);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Stock = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Rate = c.Int(nullable: false),
                        SeoKeywords = c.String(),
                        SeoDescription = c.String(),
                        SeoUrl = c.String(),
                        SeoTitle = c.String(),
                        Tax = c.Double(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        CampaingId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Campaings", t => t.CampaingId)
                .Index(t => t.CategoryId)
                .Index(t => t.CampaingId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Campaings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        Discount = c.Double(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        StopDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        MainCategoryId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        CompaignId = c.Int(nullable: false),
                        Campaing_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Campaings", t => t.Campaing_Id)
                .Index(t => t.Campaing_Id);
            
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Rate = c.Int(nullable: false),
                        Image = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        Date = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        InvoiceType = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        Page = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SiteComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                        View = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Supports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        SupportStatus = c.String(),
                        View = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        Email1 = c.String(),
                        Email2 = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                        Youtube = c.String(),
                        Linkedin = c.String(),
                        Pinterest = c.String(),
                        GoogleMaps = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactMails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Subject = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        View = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewsGets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Email = c.String(),
                        View = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        LitleDescription = c.String(),
                        Description = c.String(),
                        SeoDescription = c.String(),
                        SeoTitle = c.String(),
                        SeoUrl = c.String(),
                        SeoKeywords = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        GoogleAnalityc = c.String(),
                        YandexMetrica = c.String(),
                        Keywords = c.String(),
                        Ico = c.String(),
                        Logo = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Link = c.String(),
                        Status = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supports", "UserId", "dbo.Users");
            DropForeignKey("dbo.SiteComments", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Invoices", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Favorites", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CampaingId", "dbo.Campaings");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Campaing_Id", "dbo.Campaings");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Coupons", "UserId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "PermissionId", "dbo.Permissions");
            DropForeignKey("dbo.Orders", "CargoId", "dbo.Cargoes");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Supports", new[] { "UserId" });
            DropIndex("dbo.SiteComments", new[] { "UserID" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Invoices", new[] { "UserId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.ProductComments", new[] { "ProductId" });
            DropIndex("dbo.ProductComments", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "Campaing_Id" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CampaingId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Favorites", new[] { "ProductId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Coupons", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "GradeId" });
            DropIndex("dbo.Users", new[] { "PermissionId" });
            DropIndex("dbo.Orders", new[] { "PermissionId" });
            DropIndex("dbo.Orders", new[] { "CargoId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.Sliders");
            DropTable("dbo.Seos");
            DropTable("dbo.Pages");
            DropTable("dbo.NewsGets");
            DropTable("dbo.ContactMails");
            DropTable("dbo.Contacts");
            DropTable("dbo.Supports");
            DropTable("dbo.SiteComments");
            DropTable("dbo.Logs");
            DropTable("dbo.Invoices");
            DropTable("dbo.Grades");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductComments");
            DropTable("dbo.Categories");
            DropTable("dbo.Campaings");
            DropTable("dbo.Brands");
            DropTable("dbo.Products");
            DropTable("dbo.Favorites");
            DropTable("dbo.Coupons");
            DropTable("dbo.Users");
            DropTable("dbo.Permissions");
            DropTable("dbo.Cargoes");
            DropTable("dbo.Orders");
            DropTable("dbo.Addresses");
        }
    }
}
