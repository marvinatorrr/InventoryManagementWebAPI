using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using System.Text;
using InventoryManagementWebAPI.Interfaces;

namespace InventoryManagementWebAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        IDataContext dataContext;

        public EmployeeController(IDataContext datacontext)
        {
            dataContext = datacontext;
        }

        public EmployeeController()
        {
            dataContext = new DataContext();
        }

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
            LogTransaction(product, product.quantity, InvoiceType.Shipment);
            return Ok($"Successfully received {product.Name}. Quantity: {product.quantity}. Product ID: {product.ID}");
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
                LogTransaction(product, quantity, InvoiceType.Shipment);
                return Ok($"Successfully received Product ID: {id}. Quantity: {quantity}. Current Stock: {product.quantity}");
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
                LogTransaction(product, quantity, InvoiceType.Sale);
                return Ok($"Successfully sold Product ID: {id}. Quantity: {quantity}. Current Stock: {product.quantity}");
            }
            else
            {
                return BadRequest("Insuffient Inventory to Fulfill Request");
            }
        }

        private void LogTransaction(Product product, int quantity, InvoiceType invoiceType)
        {
            Invoice invoice = new Invoice() { TransactionTime = DateTime.Now, AssociatedEmployee = GetUserFromAuthHeaer(), Type = invoiceType.ToString() };

            invoice.Cost = product.BasePrice * quantity;
            invoice.Revenue = product.MSRP * quantity;
            invoice.ItemID = product.ID;
            invoice.Quantity = quantity;
            invoice.Taxes = invoice.Revenue * 0.07M;
            invoice.Profit = invoice.Revenue - invoice.Cost;

            dataContext.Invoices.Add(invoice);
            dataContext.SaveChanges();
        }

        private User GetUserFromAuthHeaer()
        {
            string id = RequestContext.Principal.Identity.Name;
            return dataContext.Users.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
