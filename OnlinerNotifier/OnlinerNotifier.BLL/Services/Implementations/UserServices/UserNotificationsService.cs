using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations.UserServices
{
    public class UserNotificationsService : IUserNotificationsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailValidator emailValidator;

        public UserNotificationsService(IUnitOfWork unitOfWork, IEmailValidator emailValidator)
        {
            this.unitOfWork = unitOfWork;
            this.emailValidator = emailValidator;
        }

        public bool SetNotificationParameters(int userId, NotificationParametersModel parameters)
        {
            var user = unitOfWork.Users.Get(userId);
            if (user == null || !emailValidator.IsValid(parameters.Email))
            {
                return false;
            }
            user.NotificationTime = parameters.Time;
            user.Email = parameters.Email;
            user.EnableNotifications = true;
            unitOfWork.Save();
            return true;
        }

        public bool DisableNotifications(int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            if (user == null)
            {
                return false;
            }
            user.EnableNotifications = false;
            unitOfWork.Save();
            return true;
        }
    }
}
