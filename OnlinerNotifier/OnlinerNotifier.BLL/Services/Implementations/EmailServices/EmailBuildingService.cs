using System.Collections.Generic;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.EmailServices;
using OnlinerNotifier.BLL.Templates.Builders;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.EmailServices
{
    public class EmailBuildingService : IEmailBuildingService
    {
        private readonly IEmailMapper emailMapper;

        private readonly IPriceChangesEmailBuilder priceChangesEmailBuilder;

        public EmailBuildingService(IEmailMapper emailMapper, IPriceChangesEmailBuilder priceChangesEmailBuilder)
        {
            this.emailMapper = emailMapper;
            this.priceChangesEmailBuilder = priceChangesEmailBuilder;
        }

        public NotificationEmailModel GetEmail(User user, List<NotificationProductChangesModel> priceChanges)
        {
            return emailMapper.ToModel(user, priceChangesEmailBuilder.GetMailBody(priceChanges), "Price changes.");
        }
    }
}
