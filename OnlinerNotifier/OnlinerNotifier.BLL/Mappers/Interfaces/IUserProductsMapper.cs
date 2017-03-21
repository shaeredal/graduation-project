using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IUserProductsMapper
    {
        UserProduct ToDomain(User user, Product product);
        UserProductViewModel ToModel(UserProduct userProduct);
    }
}