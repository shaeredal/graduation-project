using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.UserProductServices;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;
using OnlinerNotifier.DAL.Repositories.Interfaces;

namespace OnlinerNotifier.BLL_Tests.Services.ProductServiceTests
{
    [TestFixture]
    public class ProductServiceTestBase
    {
        protected Mock<IUnitOfWork> unitOfWorkMock;
        protected Mock<User> userMock;
        protected Mock<Product> productMock;
        protected Mock<IUserRepository> userRepositoryMock;
        protected Mock<IRepository<Product>> productRepositoryMock;
        protected UserProductService productService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
            userMock = mockStorage.UserMock;
            productMock = mockStorage.ProductMock;
            productRepositoryMock = mockStorage.ProductRepositoryMock;
            userRepositoryMock = mockStorage.UserRepositoryMock;
            var productMapper = new ProductMapper();
            productService = new UserProductService(
                new UserProductGetService(unitOfWorkMock.Object, productMapper),
                new UserProductAddService(unitOfWorkMock.Object, productMapper, new UserProductsMapper(productMapper)),
                new UserProductRemoveService(unitOfWorkMock.Object));
        }
    }
}
