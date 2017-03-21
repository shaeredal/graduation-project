using System.Collections.Generic;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class NotificationProductChangesModelMapper : INotificationProductChangesModelMapper
    {
        public NotificationProductChangesModel ToModel(Product product, List<ProductPriceChange> productPriceChange)
        {
            return new NotificationProductChangesModel()
            {
                Product = product,
                Changes = productPriceChange
            };
        }
    }
}
