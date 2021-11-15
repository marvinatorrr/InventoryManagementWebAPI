using InventoryManagementWebAPI.DBContext;
using System;
using InventoryManagementWebAPI.DBModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using InventoryManagementWebAPI.Auth;
using System.Web;
using System.Text;
using InventoryManagementWebAPI.Interfaces;
namespace InventoryManagementWebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        IDataContext dataContext;

        public AccountController(IDataContext datacontext)
        {
            dataContext = datacontext;
        }

        public AccountController()
        {
            dataContext = new DataContext();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAccount([FromBody] User user)
        {
            if (!dataContext.Users.Any(x => x.Id == user.Id))
            {
                dataContext.Users.Add(new User() { Name = user.Name, Id = user.Id, Password = Hash.HashFunction(user.Password), Type = UserType.Employee.ToString() });
                dataContext.SaveChanges();
                return Ok("User Successfully Created");
            }
            else
            {
                return BadRequest("User ID Already Exists");
            }
        }


        [Authorize(Roles = "Employee,Owner")]
        [HttpGet]
        [Route("login")]
        public IHttpActionResult Login()
        {
            string id = RequestContext.Principal.Identity.Name;
            return Ok(dataContext.Users.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}
