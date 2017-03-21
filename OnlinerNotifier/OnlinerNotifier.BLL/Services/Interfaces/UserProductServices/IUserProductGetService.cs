using System.Collections.Generic;
using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.UserProductServices
{
    public interface IUserProductGetService
    {
        List<ProductViewModel> GetUserProducts(int userId);
    }
}
