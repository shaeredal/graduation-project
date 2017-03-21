using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.EmailServices
{
    public interface IEmailModelSender
    {
        void Send(NotificationEmailModel emailModel);
    }
}
