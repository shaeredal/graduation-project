using System.Linq;
using OnlinerNotifier.BLL.Models.OnlinerDataModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.PriceChangesServices;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations.PriceChangesServices
{
    public class PricesChangesInfoService : IPricesChangesInfoService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IOnlinerSearchService onlinerSearchService;

        private readonly IPriceChangesService priceChangesService;

        public PricesChangesInfoService(IUnitOfWork unitOfWork, IOnlinerSearchService onlinerSearchService, IPriceChangesService priceChangesService)
        {
            this.unitOfWork = unitOfWork;
            this.onlinerSearchService = onlinerSearchService;
            this.priceChangesService = priceChangesService;
        }

        public void Update()
        {
            var products = unitOfWork.Products.GetAll().ToList();
            foreach (var product in products)
            {
                var onlinerProduct = GetOnlinerProduct(product.Name, product.OnlinerId);
                if (onlinerProduct != null)
                {
                    priceChangesService.CompareAndUpdate(product.Id, onlinerProduct.Prices);
                }               
            }
        }

        private ProductOnliner GetOnlinerProduct(string name, int onlinerId)
        {
            var searchResult = onlinerSearchService.Search(name);
            return FindProduct(searchResult, onlinerId);
        }

        private ProductOnliner FindProduct(SearchResultOnliner searchResult, int id)
        {
            return searchResult.Products.FirstOrDefault(prod => prod.Id == id);
        }
    }
}
