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

namespace WebAPITest
{
    public class AdminControllerTest
    {
        [Fact]
        public void ViewAllItems()
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

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewAllItems();
            var contentResult = res as OkNegotiatedContentResult<IEnumerable<Product>>;

            Assert.Contains(product, contentResult.Content);
        }

        [Fact]
        public void ViewAllItemsEmpty()
        {
            var data = new List<Product>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Products).Returns(mockSet.Object);

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewAllItems();
            var contentResult = res as OkNegotiatedContentResult<string>;

            Assert.Equal("No Products In Warehouse", contentResult.Content);
        }

        [Fact]
        public void ViewEmployeeTransactions()
        {
            Invoice i1 = new Invoice()
            {
                AssociatedEmployee = new User() { Id = "marv" },
                ID = 1,
                ItemID = 1,
                Quantity = 5
            };

            Invoice i2 = new Invoice()
            {
                AssociatedEmployee = new User() { Id = "marv" },
                ID = 2,
                ItemID = 2,
                Quantity = 10
            };

            var data = new List<Invoice>
            {
                i1,
                i2
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Invoice>>();
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Invoices).Returns(mockSet.Object);

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewEmployeeTransactions("marv");
            var contentResult = res as OkNegotiatedContentResult<List<Invoice>>;

            Assert.Equal(new List<Invoice>() { i1, i2 }, contentResult.Content);
        }

        [Fact]
        public void ViewEmployeeTransactionsEmpty()
        {
            var data = new List<Invoice>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Invoice>>();
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Invoices).Returns(mockSet.Object);

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewEmployeeTransactions("marv");

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }


        [Fact]
        public void ViewDailyTransactions()
        {
            DateTime date = new DateTime(2021, 1, 1);

            Invoice i1 = new Invoice()
            {
                AssociatedEmployee = new User() { Id = "marv" },
                ID = 1,
                ItemID = 1,
                Quantity = 5,
                TransactionTime = date
            };

            Invoice i2 = new Invoice()
            {
                AssociatedEmployee = new User() { Id = "marv" },
                ID = 2,
                ItemID = 2,
                Quantity = 10,
                TransactionTime = date
            };

            var data = new List<Invoice>
            {
                i1,
                i2
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Invoice>>();
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Invoices).Returns(mockSet.Object);

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewDailyTransactions(date);
            var contentResult = res as OkNegotiatedContentResult<List<Invoice>>;

            Assert.Equal(new List<Invoice>() { i1, i2 }, contentResult.Content);
        }

        [Fact]
        public void ViewDailyTransactionsEmpty()
        {
            var data = new List<Invoice>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Invoice>>();
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Invoice>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<DataContext>();
            mockContext.Setup(x => x.Invoices).Returns(mockSet.Object);

            AdminController ac = new AdminController(mockContext.Object);
            IHttpActionResult res = ac.ViewDailyTransactions(new DateTime(2021,1,1));

            Assert.IsType<BadRequestErrorMessageResult>(res);
        }
    }
}
