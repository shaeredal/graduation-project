using System.Collections.Generic;
using System.Net.Mail;
using Moq;
using NUnit.Framework;
using OnlinerNotifier.BLL.Mappers.Implementations;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Implementations;
using OnlinerNotifier.BLL.Services.Implementations.EmailServices;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;
using OnlinerNotifier.BLL.SmtpClients;
using OnlinerNotifier.BLL.Templates.Builders;
using OnlinerNotifier.BLL.Templates.TemplatePathProvider;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.BLL_Tests.Moq;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL_Tests.Services
{
    [TestFixture]
    public class EmailSendingServiceTest
    {
        private IEmailModelSender _emailSendingModelSender;
        private Mock<User> userMock;
        private Mock<ISmtpClient> smtpClientWrapper;
        private IEmailBuildingService emailBuilderService;

        [SetUp]
        public void Setup()
        {
            var mockStorage = new MockStorage();
            userMock = mockStorage.UserMock;
            smtpClientWrapper = mockStorage.SmtpClientMock;
            var templatePathProviderMock = new Mock<ITemplatePathProvider>();
            templatePathProviderMock.Setup(m => m.GetEmailTemplatePath())
                .Returns(() => "D:\\test-task\\OnlinerNotifier\\OnlinerNotifier.BLL\\Templates\\EmailTemplate.cshtml");
            emailBuilderService = new EmailBuildingService(new EmailMapper(new TimeCalculationService()), new RazorPriceChangesEmailBuilder(templatePathProviderMock.Object));
            _emailSendingModelSender = new EmailModelSender(new EmailValidator(), smtpClientWrapper.Object);
        }

        [Test]
        public void SendChanges_EmailSending_SendEmail()
        {
            var email = emailBuilderService.GetEmail(userMock.Object, new List<NotificationProductChangesModel>());

            _emailSendingModelSender.Send(email);

            smtpClientWrapper.Verify(m => m.Send(It.IsAny<MailMessage>()), Times.Once);
        }
    }
}
