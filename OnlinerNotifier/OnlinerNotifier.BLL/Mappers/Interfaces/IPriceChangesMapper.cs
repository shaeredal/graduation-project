using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IPriceChangesMapper
    {
        ProductPriceChange ToDomain(Product product, SearchPrice newPrice);
    }
}