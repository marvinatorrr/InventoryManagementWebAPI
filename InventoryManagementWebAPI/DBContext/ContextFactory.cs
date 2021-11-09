using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace InventoryManagementWebAPI.DBContext
{
    public class ContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext Create()
        {
            return new DataContext();
        }
    }
}