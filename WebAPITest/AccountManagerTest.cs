using InventoryManagementWebAPI.Controllers;
using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using InventoryManagementWebAPI.DTOs;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;
using Newtonsoft.Json;
namespace WebAPITest
{
    public class AccountManagerTest
    {
        [Fact]
        public void CreateAccount_Successful()
        {
            var data = new List<User>
            {

            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            AccountController ac = new AccountController(mockContext.Object);
            IHttpActionResult res = ac.CreateAccount(new User() { Id = "marv", Name = "marv", Password = "marv", Type = "Employee" });

            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public void CreateAccount_Failed()
        {
            var data = new List<User>
            {
                new User(){ Id = "marv"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            AccountController ac = new AccountController(mockContext.Object);
            IHttpActionResult res = ac.CreateAccount(new User() { Id = "marv", Name = "marv", Password = "marv", Type = "Employee" });

            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [Fact]
        public void Login()
        {
            User user = new User() { Id = "marv" };

            UserDTO userdto = new UserDTO(user);

            var data = new List<User>
            {
                user
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Users).Returns(mockSet.Object);

            AccountController ac = new AccountController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.Login();
            var contentResult = res as OkNegotiatedContentResult<UserDTO>;

            var expected = JsonConvert.SerializeObject(userdto);
            var actual = JsonConvert.SerializeObject(contentResult.Content);

            Assert.Equal(expected,actual);
        }
    }
}
