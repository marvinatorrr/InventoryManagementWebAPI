namespace InventoryManagementWebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InventoryManagementWebAPI.DBContext.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "InventoryManagementWebAPI.DBContext.DataContext";
        }

        protected override void Seed(InventoryManagementWebAPI.DBContext.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
