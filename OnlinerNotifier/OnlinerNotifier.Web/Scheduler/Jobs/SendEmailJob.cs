using System;
using FluentScheduler;
using OnlinerNotifier.BLL.Redis;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces.RedisEmailServices;
using StackExchange.Redis;

namespace OnlinerNotifier.Scheduler.Jobs
{
    public class SendEmailJob : IJob
    {
        private readonly object lockObject = new object();
             
        private readonly IRedisEmailGetter redisService;

        private readonly IEmailModelSender emailModelSender;

        private readonly IDatabase redis;

        private readonly ISubscriber sub;

        public SendEmailJob(IRedisEmailGetter redisService, IEmailModelSender emailModelSender)
        {
            this.redisService = redisService;
            this.emailModelSender = emailModelSender;
            redis = RedisConnector.Connection.GetDatabase();
            sub = RedisConnector.Connection.GetSubscriber();
        }

        public void Execute()
        {
            sub.Subscribe("notification email", (channel, key) =>
            {
                SetJob((int)key);
            });
        }

        private void SetJob(int key)
        {
            var notificationTime = new DateTime((long)redis.StringGet($"notification email:{key}:time"));
            JobManager.AddJob(() =>
            {
                lock (lockObject)
                {
                    var email = redisService.GetEmail(key);
                    emailModelSender.Send(email);
                }
            }, (s) => s.ToRunOnceAt(notificationTime));
        }
    }
}