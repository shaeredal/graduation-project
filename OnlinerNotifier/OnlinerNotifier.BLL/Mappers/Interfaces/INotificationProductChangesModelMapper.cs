using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface INotificationProductChangesModelMapper
    {
        NotificationProductChangesModel ToModel(Product product, List<ProductPriceChange> productPriceChange);
    }
}