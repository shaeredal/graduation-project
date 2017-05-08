namespace OnlinerNotifier.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductPriceChanges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldMinPrice = c.Decimal(precision: 18, scale: 2),
                        OldMaxPrice = c.Decimal(precision: 18, scale: 2),
                        NewMinPrice = c.Decimal(precision: 18, scale: 2),
                        NewMaxPrice = c.Decimal(precision: 18, scale: 2),
                        CheckTime = c.DateTime(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatalogId = c.Int(nullable: false),
                        CatalogName = c.String(),
                        Name = c.String(),
                        Image = c.String(),
                        Url = c.String(),
                        MaxPrice = c.Decimal(precision: 18, scale: 2),
                        MinPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsTracked = c.Boolean(nullable: false),
                        Product_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AvatarUri = c.String(),
                        Email = c.String(),
                        SocialId = c.String(),
                        ProviderName = c.String(),
                        NotificationTime = c.DateTime(nullable: false),
                        EnableNotifications = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProducts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductPriceChanges", "Product_Id", "dbo.Products");
            DropIndex("dbo.UserProducts", new[] { "User_Id" });
            DropIndex("dbo.UserProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductPriceChanges", new[] { "Product_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductPriceChanges");
        }
    }
}
