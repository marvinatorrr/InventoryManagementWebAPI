using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementWebAPI.Controllers
{
    [Authorize(Roles = "Owner")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        DataContext dataContext = new DataContext();

        [HttpGet]
        [Route("viewallitems")]
        public IHttpActionResult ViewAllItems()
        {
            IEnumerable<Product> products = dataContext.Products.ToList();
            if (products.Count() > 0)
            {
                return Ok(products);
            }
            else
            {
                return Ok("No Products In Warehouse");
            }
        }

        [HttpGet]
        [Route("viewtransactionsbyemp")]
        public IHttpActionResult ViewEmployeeTransactions(string empID)
        {
            IEnumerable<Invoice> invoices = dataContext.Invoices.Where(x => x.AssociatedEmployee.Id == empID);  
            return Ok(invoices);
        }

        [HttpGet]
        [Route("viewtransactionsbydate")]
        public IHttpActionResult ViewDailyTransactions(DateTime date)
        {
            List<Invoice> invoices = new List<Invoice>();

            bool found = false;
            foreach (Invoice invoice in dataContext.Invoices.ToList())
            {
                if (invoice.TransactionTime.Date == date)
                {
                    found = true;
                    invoices.Add(invoice);
                }
            }
            if (!found)
            {
                return Ok("No records found");
            }

            return Ok(invoices);
        }
    }
}
