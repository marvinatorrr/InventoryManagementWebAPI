using InventoryManagementWebAPI.DBContext;
using System;
using InventoryManagementWebAPI.DBModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace InventoryManagementWebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        DataContext dataContext = new DataContext();

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount(string name, string Id, string password)
        {
            if (!dataContext.Users.Any(x => x.Id == Id))
            {
                dataContext.Users.Add(new User() { Name = name, Id = Id, Password = password, Type = UserType.Employee.ToString() });
                dataContext.SaveChanges();
                return Ok("User Successfully Created");
            }
            else
            {
                return BadRequest("User ID Already Exists");
            }
        }

        [HttpGet]
        [Route("login")]
        public IHttpActionResult Login(string Id, string password)
        {
            var user = dataContext.Users.Where(x => x.Id == Id && x.Password == password);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Crendentials");
            }
        }
    }
}
