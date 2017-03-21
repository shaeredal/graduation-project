using System.Collections.Generic;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;

namespace OnlinerNotifier.BLL.Services.Implementations.UserProductServices
{
    public class UserProductService : IUserProductService
    {
        private IUserProductGetService getService;

        private IUserProductAddService addService;

        private IUserProductRemoveService removeService;

        public UserProductService(IUserProductGetService getService,
            IUserProductAddService addService,
            IUserProductRemoveService removeService)
        {
            this.getService = getService;
            this.addService = addService;
            this.removeService = removeService;
        }

        public List<ProductViewModel> GetUserProducts(int userId)
        {
            return getService.GetUserProducts(userId);
        }

        public bool AddUserProduct(ProductViewModel product, int userId)
        {
            return addService.AddToUser(product, userId);
        }

        public bool RemoveUserProduct(int userId, int productId)
        {
            return removeService.RemoveFromUser(userId, productId);
        }
    }
}
