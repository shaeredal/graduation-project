using System.Collections.Generic;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class NotificationDataModelMapper : INotificationDataModelMapper
    {
        public NotificationDataModel ToModel(User user,
            List<NotificationProductChangesModel> notificationProductChanges)
        {
            return new NotificationDataModel()
            {
                User = user,
                Products = notificationProductChanges
            };
        }
    }
}
