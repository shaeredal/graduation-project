using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.RedisEmailServices
{
    public interface IRedisEmailPublisher
    {
        void PublishEmail(NotificationEmailModel emailModel);
    }
}