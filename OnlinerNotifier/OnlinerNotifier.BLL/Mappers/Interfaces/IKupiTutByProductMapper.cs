using OnlinerNotifier.BLL.Models.SearchDataModels;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IKupiTutByProductMapper
    {
        SearchProduct ToSearchProduct(KupiTutByProduct product);
    }
}
