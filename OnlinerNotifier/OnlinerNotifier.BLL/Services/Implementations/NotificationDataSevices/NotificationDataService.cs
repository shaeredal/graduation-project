using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices
{
    public class NotificationDataDataService : INotificationDataService
    {
        private readonly INotifiableUsersProvider notifiableUsersProvider;

        private readonly IUserProductChangesService userProductChangesService;

        private readonly INotificationDataModelMapper notificationDataModelMapper;

        public NotificationDataDataService(INotifiableUsersProvider notifiableUsersProvider,
            IUserProductChangesService userProductChangesService,
            INotificationDataModelMapper notificationDataModelMapper)
        {
            this.notifiableUsersProvider = notifiableUsersProvider;
            this.userProductChangesService = userProductChangesService;
            this.notificationDataModelMapper = notificationDataModelMapper;
        }

        public List<NotificationDataModel> CollectNotificationData(TimeSpan period)
        {
            var users = notifiableUsersProvider.GetNotifiableUsers();
            return users.Select(user => GetUserNotifications(user, period)).Where(userData => userData.Products.Any()).ToList();
        }

        private NotificationDataModel GetUserNotifications(User user, TimeSpan period)
        {
            return notificationDataModelMapper.ToModel(user, userProductChangesService.GetProductChanges(user, period));
        }
    }
}
