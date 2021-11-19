using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementWebAPI.DBModels;
namespace InventoryManagementWebAPI.DTOs
{
    public class UserDTO
    {
        public string Name { get;  set; }
        public string Id { get;  set; }
        public string Type { get;  set; }

        public UserDTO(User user)
        {
            Name = user.Name;
            Id = user.Id;
            Type = user.Type;
        }
    }
}