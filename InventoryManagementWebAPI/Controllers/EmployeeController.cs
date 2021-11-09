using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;

namespace InventoryManagementWebAPI.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        DataContext dataContext = new DataContext();

        [HttpGet]
        [Route("getproductbyid")]
        public IHttpActionResult GetProductByID(int id)
        {
            Product product = dataContext.Products.Where(x => x.ID == id).FirstOrDefault();
            if(product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Invalid Product ID");
            }
        }

        [HttpGet]
        [Route("getproductbyname")]
        public IHttpActionResult GetProductByName(string name)
        {
            Product product = dataContext.Products.Where(x => x.Name == name).FirstOrDefault();
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest("Invalid Product Name");
            }
        }

        [HttpPost]
        [Route("receivenewproduct")]
        public IHttpActionResult ReceiveNewProduct(Product product)
        {
            dataContext.Products.Add(product);
            dataContext.SaveChanges();
            return Ok("Product added");
        }

        [HttpPatch]
        [Route("receiveexistingproduct")]
        public IHttpActionResult ReceiveExistingProduct(int id, int quantity)
        {
            Product product = dataContext.Products.Where(x => x.ID == id).FirstOrDefault();
            if (product != null && quantity > 0)
            {
                product.quantity = product.quantity + quantity;
                dataContext.Entry(product).State = EntityState.Modified;
                dataContext.SaveChanges();
                return Ok("Product added");
            }
            else
            {
                return BadRequest("Invalid Product ID");
            }
            
        }

        [HttpPatch]
        [Route("sellproduct")]
        public IHttpActionResult SellProduct(int id, int quantity)
        {
            Product product = dataContext.Products.Where(x => x.ID == id).FirstOrDefault();
            if (product == null)
            {
                return BadRequest("Invalid Product ID");
            }
            else if (product.quantity >= quantity)
            {
                product.quantity = product.quantity - quantity;
                dataContext.Entry(product).State = EntityState.Modified;
                dataContext.SaveChanges();
                return Ok("Product sold");
            }
            else
            {
                return BadRequest("Insuffient Inventory to Fulfill Request");
            }
        }

        private void LogTransaction(Product product, int quantity, InvoiceType invoiceType, User user)
        {
            Invoice invoice = new Invoice() { TransactionTime = DateTime.Now, AssociatedEmployee = user, Type = invoiceType };

            invoice.Cost = product.BasePrice * quantity;
            invoice.Revenue = product.MSRP * quantity;
            invoice.ItemID = product.ID;
            invoice.Quantity = quantity;
            invoice.Taxes = invoice.Revenue * 0.07M;
            invoice.Profit = invoice.Revenue - invoice.Cost;

            dataContext.Invoices.Add(invoice);
            dataContext.SaveChanges();
        }
    }
}
