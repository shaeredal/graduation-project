using OnlinerNotifier.BLL.Models.SearchDataModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices
{
    public interface IPriceChangesService
    {
        void CompareAndUpdate(int productId, SearchPrice newPrice);
    }
}