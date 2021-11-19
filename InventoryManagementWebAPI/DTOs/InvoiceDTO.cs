using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventoryManagementWebAPI.DBModels;

namespace InventoryManagementWebAPI.DTOs
{
    public class InvoiceDTO
    {
        public int ID { get; set; }
        public DateTime TransactionTime { get;  set; }
        public virtual User AssociatedEmployee { get;  set; }
        public int ItemID { get;  set; }
        public int Quantity { get;  set; }
        public decimal Taxes { get;  set; }
        public decimal Revenue { get;  set; }
        public decimal Cost { get;  set; }
        public decimal Profit { get;  set; }
        public string Type { get;  set; }

        public InvoiceDTO(Invoice invoice)
        {
            ID = invoice.ID;
            TransactionTime = invoice.TransactionTime;
            AssociatedEmployee = invoice.AssociatedEmployee;
            ItemID = invoice.ItemID;
            Quantity = invoice.Quantity;
            Taxes = invoice.Taxes;
            Revenue = invoice.Revenue;
            Cost = invoice.Cost;
            Profit = invoice.Profit;
            Type = invoice.Type;
        }
    }
}