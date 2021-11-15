using Xunit;
using InventoryManagementWebAPI.Controllers;
using Moq;
using InventoryManagementWebAPI.Interfaces;
using InventoryManagementWebAPI.DBModels;
namespace WebAPITestProject
{
    public class UnitTest1
    {
        [Fact]
        public void CreateUserSuccessful()
        {
            var mockdb = new Mock<IDataContext>();
            AccountController accountcontroller = new AccountController(mockdb.Object);
            var res = accountcontroller.CreateAccount(new User() { Name = "Marvin", Id = "marv", Password = "marv" });

        }
    }
}
