using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagementWebAPI.DTOs
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal BasePrice { get; set; }
        public decimal MSRP { get; set; }
        public int quantity { get; set; }

        public ProductDTO()
        {

        }
        public ProductDTO(Product product)
        {
            ID = product.ID;
            Name = product.Name;
            Manufacturer = product.Manufacturer;
            ManufactureDate = product.ManufactureDate;
            ExpiryDate = product.ExpiryDate;
            BasePrice = product.BasePrice;
            MSRP = product.MSRP;
            quantity = product.quantity;
        }

    }
}