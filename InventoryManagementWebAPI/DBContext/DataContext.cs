using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InventoryManagementWebAPI.DBModels;

namespace InventoryManagementWebAPI.DBContext
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }

    }
}