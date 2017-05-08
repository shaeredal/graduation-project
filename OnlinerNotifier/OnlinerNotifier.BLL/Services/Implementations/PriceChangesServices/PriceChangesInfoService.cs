using System.Linq;
using OnlinerNotifier.BLL.Models.SearchDataModels;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;
using OnlinerNotifier.BLL.Services.Interfaces.SearchServices;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations.PriceChangesServices
{
    public class PricesChangesInfoService : IPricesChangesInfoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOnlinerSearchService onlinerSearchService;
        private readonly IKupiTutBySearchService kupiTutBySearchService;
        private readonly IOneKSearchService oneKSearchService;
        private readonly IPriceChangesService priceChangesService;

        public PricesChangesInfoService(IUnitOfWork unitOfWork, 
            IOnlinerSearchService onlinerSearchService,
            IKupiTutBySearchService kupiTutBySearchService, 
            IOneKSearchService oneKSearchService,
            IPriceChangesService priceChangesService)
        {
            this.unitOfWork = unitOfWork;
            this.onlinerSearchService = onlinerSearchService;
            this.kupiTutBySearchService = kupiTutBySearchService;
            this.oneKSearchService = oneKSearchService;
            this.priceChangesService = priceChangesService;
        }

        public void Update()
        {
            var products = unitOfWork.Products.GetAll().ToList();
            foreach (var product in products)
            {
                var searchProduct = GetSearchProduct(product.Name, product.CatalogId, product.CatalogName);
                if (searchProduct != null)
                {
                    priceChangesService.CompareAndUpdate(product.Id, searchProduct.Prices);
                }               
            }
        }

        private SearchProduct GetSearchProduct(string name, int onlinerId, string catalogName)
        {
            SearchResult searchResult;
            switch (catalogName)
            {
                case "onliner.by":
                    searchResult = onlinerSearchService.Search(name);
                    break;
                case "kupi.tut.by":
                    searchResult = kupiTutBySearchService.Search(name);
                    break;
                case "1k.by":
                    searchResult = oneKSearchService.Search(name);
                    break;
                default:
                    return null;
            }
            return FindProduct(searchResult, onlinerId);
        }

        private SearchProduct FindProduct(SearchResult searchResult, int id)
        {
            return searchResult.Products.FirstOrDefault(prod => prod.Id == id);
        }
    }
}
