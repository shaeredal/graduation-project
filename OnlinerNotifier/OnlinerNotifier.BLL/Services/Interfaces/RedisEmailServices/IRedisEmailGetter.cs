using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.RedisEmailServices
{
    public interface IRedisEmailGetter
    {
        NotificationEmailModel GetEmail(int key);
    }
}
