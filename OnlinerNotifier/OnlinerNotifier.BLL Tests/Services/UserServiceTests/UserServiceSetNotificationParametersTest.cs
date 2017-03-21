using System;
using NUnit.Framework;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Implementations.UserServices;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.BLL.Validators;

namespace OnlinerNotifier.BLL_Tests.Services.UserServiceTests
{
    public class UserServiceSetNotificationParametersTest : UserServiceTestBase
    {
        private IUserNotificationsService notificationsService;

        [SetUp]
        public new void Setup()
        {
            notificationsService = new UserNotificationsService(unitOfWorkMock.Object, new EmailValidator());
        }

        [TestCase(1, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = false)]
        public bool SetNotificationParameters_ReturnValue_IsCorrect(int userId)
        {
            var notificationParameters = new NotificationParametersModel()
            {
                Email = "OnlinerNotifier@gmail.com",
                Time = new DateTime()
            };

            var result = notificationsService.SetNotificationParameters(userId, notificationParameters);

            return result;
        }
    }
}
