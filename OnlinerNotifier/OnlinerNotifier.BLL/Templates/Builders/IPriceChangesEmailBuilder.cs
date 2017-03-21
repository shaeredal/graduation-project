using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;

namespace OnlinerNotifier.BLL.Templates.Builders
{
    public interface IPriceChangesEmailBuilder
    {
        string GetMailBody(List<NotificationProductChangesModel> priceChanges);
    }
}
