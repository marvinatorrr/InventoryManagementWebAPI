using System;
using Xunit;
using InventoryManagementWebAPI.Controllers;
using Moq;
using InventoryManagementWebAPI.Interfaces;
using InventoryManagementWebAPI.DBModels;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data.Entity;
using InventoryManagementWebAPI.DBContext;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

namespace WebAPITest
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void GetProductByID()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);
            IHttpActionResult res = ac.GetProductByID(1);
            var contentResult = res as OkNegotiatedContentResult<Product>;

            Assert.Equal(product, contentResult.Content);
        }


        [Fact]
        public void GetProductByID_Invalid()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);
            IHttpActionResult res = ac.GetProductByID(2);

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }

        [Fact]
        public void GetProductByName()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);
            IHttpActionResult res = ac.GetProductByName("hammer");
            var contentResult = res as OkNegotiatedContentResult<Product>;

            Assert.Equal(product, contentResult.Content);
        }

        [Fact]
        public void GetProductByName_Invalid()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);
            IHttpActionResult res = ac.GetProductByName("rubber");

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }

        [Fact]
        public void AddNewProduct()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());



            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.ReceiveNewProduct(product);
            var contentResult = res as OkNegotiatedContentResult<string>;

            Assert.Equal(
                $"Successfully received {product.Name}. " +
                $"Quantity: {product.quantity}. " +
                $"Product ID: {product.ID}",
                contentResult.Content);
        }

        [Fact]
        public void AddExistingProduct()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 1, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.ReceiveExistingProduct(1,2);
            var contentResult = res as OkNegotiatedContentResult<string>;

            Assert.Equal(
                $"Successfully received Product ID: {1}. Quantity: {2}. Current Stock: {3}"
                ,
                contentResult.Content);
        }

        [Fact]
        public void AddExistingProduct_failed()
        {
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.ReceiveExistingProduct(1, 2);

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }

        [Fact]
        public void SellProduct()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 5, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.SellProduct(1, 2);
            var contentResult = res as OkNegotiatedContentResult<string>;

            Assert.Equal(
                $"Successfully sold Product ID: {1}. Quantity: {2}. Current Stock: {3}"
                ,
                contentResult.Content);
        }

        [Fact]
        public void SellProduct_Invalid_ID()
        {
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.SellProduct(1, 2);

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }

        [Fact]
        public void SellProduct_Insufficient_Stock()
        {
            Product product = new Product() { ID = 1, Name = "hammer", quantity = 5, MSRP = 4, BasePrice = 2, Manufacturer = "borsch" };
            User user = new User() { Id = "marv" };

            var userdata = new List<User>
            {
                user
            }.AsQueryable();

            var data = new List<Product>
            {
                product
            }.AsQueryable();

            var mockInvoiceSet = new Mock<DbSet<Invoice>>();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockUsersSet = new Mock<DbSet<User>>();
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userdata.Provider);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userdata.Expression);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(userdata.ElementType);
            mockUsersSet.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(userdata.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            mockContext.Setup(x => x.Users).Returns(mockUsersSet.Object);
            mockContext.Setup(x => x.Invoices).Returns(mockInvoiceSet.Object);

            EmployeeController ac = new EmployeeController(mockContext.Object);

            ac.ControllerContext.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("marv"), new string[] { "Employee" });

            IHttpActionResult res = ac.SellProduct(1, 10);

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }
        
    }
}
