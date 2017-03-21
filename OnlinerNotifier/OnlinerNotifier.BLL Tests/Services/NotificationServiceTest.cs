using System;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL_Tests.Moq;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class NotificationServiceTest
    {
        private INotificationDataService notificationDataService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            notificationDataService = new NotificationDataDataService(new NotifiableUsersService(mockStorage.UnitOfWorkMock.Object, new EmailValidator()), 
                new UserProductChangesService(new NotificationProductChangesModelMapper()),
                new NotificationDataModelMapper());
        }

        [TestCase(0, ExpectedResult = 100)]
        [TestCase(1, ExpectedResult = 101)]
        public decimal GetNotificationData_Data_CorrectData(int index)
        {
            var data = notificationDataService.CollectNotificationData(TimeSpan.FromDays(1));
            var result = data[0].Products[0].Changes[index].NewMinPrice;
            
            return result;
        }

        [Test]
        public void GetNotificationData_Data_OlderThanDayAreNotIncluded()
        {
            var data = notificationDataService.CollectNotificationData(TimeSpan.FromDays(1));
            var result = data[0].Products[0].Changes;

            Assert.AreEqual(2, result.Count);
        }
    }
}
