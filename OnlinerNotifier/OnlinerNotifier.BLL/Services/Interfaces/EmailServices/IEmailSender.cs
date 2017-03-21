using System.Net.Mail;

namespace OnlinerNotifier.BLL.Services.Interfaces.EmailServices
{
    interface IEmailSender
    {
        void Send(MailAddress toAddress, string mailBody, string subject, bool isHtml);
    }
}
