using System.Collections.Generic;
using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.UserProductServices
{
    public interface IUserProductService
    {
        List<ProductViewModel> GetUserProducts(int userId);
        bool AddUserProduct(ProductViewModel product, int userId);
        bool RemoveUserProduct(int userId, int productId);
    }
}