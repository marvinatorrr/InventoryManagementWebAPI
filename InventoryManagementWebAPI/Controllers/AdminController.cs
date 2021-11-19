using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using InventoryManagementWebAPI.DTOs;
using InventoryManagementWebAPI.Interfaces;
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
        IDataContext dataContext;

        public AdminController(IDataContext datacontext)
        {
            dataContext = datacontext;
        }

        public AdminController()
        {
            dataContext = new DataContext();
        }

        [HttpGet]
        [Route("viewallitems")]
        public IHttpActionResult ViewAllItems()
        {
            IEnumerable<Product> products = dataContext.Products.ToList();

            List<ProductDTO> productDTOs = new List<ProductDTO>();

            foreach (Product product in products)
            {
                productDTOs.Add(new ProductDTO(product));
            }

            if (productDTOs.Count() > 0)
            {
                return Ok(productDTOs);
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
            List<InvoiceDTO> invoiceDTOs = new List<InvoiceDTO>();

            bool found = false;
            foreach (Invoice invoice in dataContext.Invoices.ToList())
            {
                if (invoice.AssociatedEmployee.Id == empID)
                {
                    found = true;
                    invoiceDTOs.Add(new InvoiceDTO(invoice));
                }
            }

            if (!found)
            {
                return BadRequest("No records found");
            }

            return Ok(invoiceDTOs);
        }

        [HttpPost]
        [Route("viewtransactionsbydate")]
        public IHttpActionResult ViewDailyTransactions([FromBody] DateTime date)
        {
            List<InvoiceDTO> invoices = new List<InvoiceDTO>();

            bool found = false;
            foreach (Invoice invoice in dataContext.Invoices.ToList())
            {
                if (invoice.TransactionTime.Date == date)
                {
                    found = true;
                    invoices.Add(new InvoiceDTO(invoice));
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
