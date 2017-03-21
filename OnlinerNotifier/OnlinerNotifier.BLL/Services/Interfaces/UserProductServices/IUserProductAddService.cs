using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.UserProductServices
{
    public interface IUserProductAddService
    {
        bool AddToUser(ProductViewModel product, int userId);
    }
}
