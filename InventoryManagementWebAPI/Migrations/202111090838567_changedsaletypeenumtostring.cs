namespace InventoryManagementWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedsaletypeenumtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "Type", c => c.Int(nullable: false));
        }
    }
}
