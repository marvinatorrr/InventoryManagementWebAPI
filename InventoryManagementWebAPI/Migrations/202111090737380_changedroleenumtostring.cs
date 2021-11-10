namespace InventoryManagementWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedroleenumtostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Type", c => c.Int(nullable: false));
        }
    }
}
