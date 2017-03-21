using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices
{
    public class UserProductChangesService : IUserProductChangesService
    {
        private readonly INotificationProductChangesModelMapper notificationProductChangesModelMapper;

        public UserProductChangesService(INotificationProductChangesModelMapper notificationProductChangesModelMapper)
        {
            this.notificationProductChangesModelMapper = notificationProductChangesModelMapper;
        }

        public List<NotificationProductChangesModel> GetProductChanges(User user, TimeSpan period)
        {
            var result = new List<NotificationProductChangesModel>();
            foreach (var product in GetTrackedProducts(user))
            {
                var changes = GetProductPriceChanges(product, period);
                if (changes.Any())
                {
                    result.Add(notificationProductChangesModelMapper.ToModel(product, changes));
                }
            }
            return result;
        }

        private List<Product> GetTrackedProducts(User user)
        {
            return user.UserProducts.Where(up => up.IsTracked).Select(up => up.Product).ToList();
        }

        private List<ProductPriceChange> GetProductPriceChanges(Product product, TimeSpan period)
        {
            return product.PriceChanges.Where(prod => DateTime.Now - prod.CheckTime < period).ToList();
        }
    }
}
