using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Services.Implementations.UserServices;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    [TestFixture]
    public class UserServiceTestBase
    {
        protected Mock<IUnitOfWork> unitOfWorkMock;
        protected Mock<IUserRepository> userRepositoryMock;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
            userRepositoryMock = mockStorage.UserRepositoryMock;
        }
    }
}
