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

namespace InventoryManagementWebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        DataContext dataContext = new DataContext();

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
            var tokens = HttpContext.Current.Request.Headers.GetValues("Authorization").FirstOrDefault();
            string[] base64encoded = tokens.Split(' ');
            byte[] data = Convert.FromBase64String(base64encoded[1]);
            string decodedString = Encoding.UTF8.GetString(data);
            string[] tokensValues = decodedString.Split(':');
            string id = tokensValues[0];
            return Ok(dataContext.Users.Where(x => x.Id == id).FirstOrDefault());
        }
    }
}
