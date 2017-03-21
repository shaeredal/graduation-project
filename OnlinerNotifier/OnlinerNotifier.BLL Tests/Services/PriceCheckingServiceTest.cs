using System.Linq;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.PriceChangesServices;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class PriceCheckingServiceTest
    {
        private Mock<IUnitOfWork> unitOfWorkMock;
        private IPricesChangesInfoService pricesChangesObserverService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            unitOfWorkMock = mockStorage.UnitOfWorkMock;
            var onlinerSearchServiceMock = mockStorage.OnlinerSearchServiceMock;
            unitOfWorkMock.Setup(m => m.PriceCanges.Create(It.IsAny<ProductPriceChange>()));

            pricesChangesObserverService = new PricesChangesInfoService(unitOfWorkMock.Object, onlinerSearchServiceMock.Object,
                new PriceChangesService(unitOfWorkMock.Object, new PriceChangesMapper()));
        }

        [Test]
        public void Check_Checking_ChecksProducts()
        {
            var testProduct = unitOfWorkMock.Object.Products.GetAll().FirstOrDefault(p => p.OnlinerId == 12345);

            pricesChangesObserverService.Update();

            unitOfWorkMock.Verify(m => m.PriceCanges.Create(It.IsAny<ProductPriceChange>()), Times.Once);
            Assert.AreEqual(70000, testProduct.MaxPrice);
            Assert.AreEqual(50000, testProduct.MinPrice);
        }
    }
}
