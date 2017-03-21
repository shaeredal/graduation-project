using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IPriceChangesMapper
    {
        ProductPriceChange ToDomain(Product product, PriceOnliner newPrice);
    }
}