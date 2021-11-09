using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementWebAPI.DBModels
{
    public class Invoice
    {
        public int ID { get; set; }
        public DateTime TransactionTime { get;  set; }
        public User AssociatedEmployee { get;  set; }
        public int ItemID { get;  set; }
        public int Quantity { get;  set; }
        public decimal Taxes { get;  set; }
        public decimal Revenue { get;  set; }
        public decimal Cost { get;  set; }
        public decimal Profit { get;  set; }
        public string Type { get;  set; }
    }
}