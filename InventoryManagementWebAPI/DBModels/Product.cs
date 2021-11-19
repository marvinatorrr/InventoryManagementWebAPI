using InventoryManagementWebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementWebAPI.DBModels
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal BasePrice { get; set; }
        public decimal MSRP { get; set; }
        public int quantity { get; set; }

        public Product()
        {

        }

        public Product(ProductDTO productdto)
        {
            ID = productdto.ID;
            Name = productdto.Name;
            Manufacturer = productdto.Manufacturer;
            ManufactureDate = productdto.ManufactureDate;
            ExpiryDate = productdto.ExpiryDate;
            BasePrice = productdto.BasePrice;
            MSRP = productdto.MSRP;
            quantity = productdto.quantity;
        }
    }
}