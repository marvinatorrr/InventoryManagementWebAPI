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
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        DataContext dataContext = new DataContext();

        [HttpGet]
        [Route("viewallitems")]
        public IEnumerable<Product> ViewAllItems()
        {
            return dataContext.Products.ToList();
        }

        [HttpGet]
        [Route("viewtransactions")]
        public IHttpActionResult ViewEmployeeTransactions(string empID)
        {
            bool found = false;
            List<Invoice> invoices = new List<Invoice>();
            foreach (Invoice invoice in dataContext.Invoices.ToList())
            {
                if (invoice.AssociatedEmployee.Id == empID)
                {
                    found = true;
                    invoices.Add(invoice);
                }
            }

            if (!found)
            {
                return BadRequest("No records found");
            }

            return Ok(invoices);
        }

        [HttpGet]
        [Route("viewtransactions")]
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
                return BadRequest("No records found");
            }

            return Ok(invoices);
        }
    }
}
