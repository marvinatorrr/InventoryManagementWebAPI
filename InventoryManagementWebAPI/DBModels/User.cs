using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementWebAPI.DBModels
{
    public class User
    {
        public string Name { get;  set; }
        public string Id { get;  set; }
        public string Password { get;  set; }
        public UserType Type { get;  set; }
    }
}