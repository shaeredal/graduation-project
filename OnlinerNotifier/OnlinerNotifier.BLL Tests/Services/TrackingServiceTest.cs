using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Models.TrackingModels;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class TrackingServiceTest
    {
        private Mock<UserProduct> userProductMock;
        private ITrackingService trackingService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            userProductMock = mockStorage.UserProductMock;

            trackingService = new TrackingService(mockStorage.UnitOfWorkMock.Object);   
        }

        [TestCase(false, ExpectedResult = false)]
        [TestCase(true, ExpectedResult = true)]
        public bool ChangeStatus_Status_IsChanged(bool status)
        {
            var trackingModel = new TrackingModel(){ Id = 1, Status = status };

            trackingService.ChangeStatus(trackingModel);
            var result = userProductMock.Object.IsTracked;

            return result;
        }
    }
}
