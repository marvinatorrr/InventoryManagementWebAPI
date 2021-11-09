using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace InventoryManagementWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //DataContext dataContext = new DataContext();

            //Product product = dataContext.Products.Where(x => x.ID == 1).FirstOrDefault();
            //product.quantity = product.quantity - 1;
            //dataContext.Entry(product).State = EntityState.Modified;
            //dataContext.SaveChanges();

            //Product product = new Product()
            //{
            //    Name = "hammer",
            //    Manufacturer = "hammermanufacturer",
            //    ExpiryDate = new DateTime(2018, 1, 1),
            //    ManufactureDate = new DateTime(2024, 1, 1),
            //    BasePrice = 5,
            //    MSRP = 10,
            //    quantity = 20
            //};

            //dataContext.Products.Add(product);
            //dataContext.SaveChanges();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
