using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IEmailMapper
    {
        NotificationEmailModel ToModel(User user, string emailBody, string subject);
    }
}
