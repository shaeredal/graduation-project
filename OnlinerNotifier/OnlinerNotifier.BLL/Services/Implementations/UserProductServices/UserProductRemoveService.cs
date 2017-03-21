using System.Linq;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.UserProductServices
{
    public class UserProductRemoveService : IUserProductRemoveService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserProductRemoveService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool RemoveFromUser(int userId, int productId)
        {
            var user = unitOfWork.Users.Get(userId);
            var product = unitOfWork.Products.Get(productId);
            if (user == null || !user.UserProducts.Select(up => up.Product).Contains(product))
            {
                return false;
            }
            return RemoveUserProduct(user, product);
        }

        private bool RemoveUserProduct(User user, Product product)
        {
            var userProduct = user.UserProducts.First(p => p.Product == product);
            unitOfWork.UserProducts.Delete(userProduct.Id);
            unitOfWork.Save();
            if (product.UserProducts.Any())
            {
                return true;
            }
            return RemoveProduct(product.Id);
        }

        private bool RemoveProduct(int productId)
        {
            if (!unitOfWork.Products.Delete(productId))
            {
                return false;
            }
            unitOfWork.Save();
            return true;
        }
    }
}