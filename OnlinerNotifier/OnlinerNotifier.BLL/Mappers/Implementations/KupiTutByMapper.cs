using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models.SearchDataModels;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class KupiTutByMapper : IKupiTutByProductMapper
    {
        public SearchProduct ToSearchProduct(KupiTutByProduct product)
        {
            return new SearchProduct
            {
                Id = product.Id,
                CatalogName = "kupi.tut.by",
                FullName = product.Name,
                PhotoUrl = product.PhotoUrl,
                Url = product.Url,
                Prices = new SearchPrice
                {
                    Min = product.PriceMin * 100
                }
            };
        }
    }
}
