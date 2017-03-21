using System;
using System.Collections.Generic;
using FluentScheduler;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.BLL.Services.Interfaces.RedisEmailServices;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class PushEmailsJob : IJob
    {
        private readonly INotificationDataService notificationDataService;

        private readonly IEmailBuildingService emailBuilderService;

        private readonly IRedisEmailPublisher redisService;

        public PushEmailsJob(INotificationDataService notificationDataService, 
            IEmailBuildingService emailBuilderService, 
            IRedisEmailPublisher redisService)
        {
            this.notificationDataService = notificationDataService;
            this.emailBuilderService = emailBuilderService;
            this.redisService = redisService;
        }

        public void Execute()
        {
            var data = GetData();
            PublishEmails(data);
        }

        private List<NotificationDataModel> GetData()
        {
            return notificationDataService.CollectNotificationData(TimeSpan.FromDays(1));
        }

        private void PublishEmails(List<NotificationDataModel> data)
        {
            foreach (var userData in data)
            {
                var email = emailBuilderService.GetEmail(userData.User, userData.Products);
                redisService.PublishEmail(email);
            }
        }
    }
}