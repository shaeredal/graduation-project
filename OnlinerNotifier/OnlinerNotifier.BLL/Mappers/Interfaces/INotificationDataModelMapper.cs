using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface INotificationDataModelMapper
    {
        NotificationDataModel ToModel(User user,
            List<NotificationProductChangesModel> notificationProductChanges);
    }
}