using Moq;
using NUnit.Framework;
using OAuth2.Models;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.UserServices;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceAddOrUpdateTest : UserServiceTestBase
    {
        private IUserManageService userManageService;

        [SetUp]
        public new void Setup()
        {
            userManageService = new UserManageService(unitOfWorkMock.Object, new UserMapper(new UserProductsMapper(new ProductMapper())));
        }

        [Test]
        public void AddOrUpdate_CreateNewUser_IsCreates()
        {
            var userInfo = new UserInfo() {Id = "42"};

            userManageService.GetOrCreate(userInfo);

            userRepositoryMock.Verify(ur =>ur.Create(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void AddOrUpdate_CreateNewUser_ReturnsId()
        {
            var userInfo = new UserInfo() { Id="not 42" };

            var result = userManageService.GetOrCreate(userInfo);

            Assert.AreEqual(42, result);
        }
    }
}
