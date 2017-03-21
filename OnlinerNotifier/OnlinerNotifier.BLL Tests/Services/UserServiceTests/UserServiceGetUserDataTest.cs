using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.UserServices;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceGetUserDataTest : UserServiceTestBase
    {
        private IUserDataService userDataService;

        [SetUp]
        public void SetUp()
        {
            userDataService = new UserDataService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));
        }

        [Test]
        public void GetUserData_UserData_GetUserData()
        {
            var result = userDataService.GetUserData(1);

            Assert.AreEqual("TestName", result.FirstName);
            Assert.AreEqual("TestLastName", result.LastName);
        }

        [Test]
        public void GetUserData_UserData_GetNull()
        {
            var result = userDataService.GetUserData(11);

            Assert.IsNull(result);
        }
    }
}
