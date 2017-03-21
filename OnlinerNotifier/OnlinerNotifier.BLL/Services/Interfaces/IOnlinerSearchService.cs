using OnlinerNotifier.BLL.Models.OnlinerDataModels;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface IOnlinerSearchService
    {
        SearchResultOnliner Search(string productName);
    }
}
