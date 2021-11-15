using InventoryManagementWebAPI.DBModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace InventoryManagementWebAPI.Interfaces
{
    public interface IDataContext
    {
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}