namespace InventoryManagementWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinvoiceid : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TransactionTime = c.DateTime(nullable: false),
                        ItemID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Taxes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Revenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Type = c.Int(nullable: false),
                        AssociatedEmployee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.AssociatedEmployee_Id)
                .Index(t => t.AssociatedEmployee_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Password = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "AssociatedEmployee_Id", "dbo.Users");
            DropIndex("dbo.Invoices", new[] { "AssociatedEmployee_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Invoices");
        }
    }
}
