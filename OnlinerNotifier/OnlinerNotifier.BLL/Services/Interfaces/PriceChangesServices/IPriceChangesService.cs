using OnlinerNotifier.BLL.Models.OnlinerDataModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices
{
    public interface IPriceChangesService
    {
        void CompareAndUpdate(int productId, PriceOnliner newPrice);
    }
}