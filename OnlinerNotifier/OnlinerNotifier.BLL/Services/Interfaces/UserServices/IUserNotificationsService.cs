using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.UserServices
{
    public interface IUserNotificationsService
    {
        bool SetNotificationParameters(int userId, NotificationParametersModel parameters);
        bool DisableNotifications(int userId);
    }
}
