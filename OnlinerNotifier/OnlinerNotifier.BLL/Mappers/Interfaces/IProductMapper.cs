using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IProductMapper
    {
        Product ToDomain(ProductViewModel model);
        ProductViewModel ToModel(Product product);
    }
}