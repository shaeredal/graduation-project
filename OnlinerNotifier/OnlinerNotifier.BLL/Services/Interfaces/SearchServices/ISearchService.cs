using OnlinerNotifier.BLL.Models.SearchDataModels;

namespace OnlinerNotifier.BLL.Services.Interfaces.SearchServices
{
    public interface ISearchService
    {
        SearchResult Search(string productName);
    }
}
