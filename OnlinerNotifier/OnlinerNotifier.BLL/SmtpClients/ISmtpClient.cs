using System.Net.Mail;

namespace OnlinerNotifier.BLL.SmtpClients
{
    public interface ISmtpClient
    {
        void Send(MailMessage message);
    }
}
