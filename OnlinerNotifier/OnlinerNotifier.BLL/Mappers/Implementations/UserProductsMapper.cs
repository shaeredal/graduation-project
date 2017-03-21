using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class UserProductsMapper : IUserProductsMapper
    {
        private readonly IProductMapper productMapper;

        public UserProductsMapper(IProductMapper productMapper)
        {
            this.productMapper = productMapper;
        }

        public UserProduct ToDomain(User user, Product product)
        {
            return new UserProduct()
            {
                User = user,
                Product = product,
                IsTracked = true
            };
        }

        public UserProductViewModel ToModel(UserProduct userProduct)
        {
            return new UserProductViewModel()
            {
                Id = userProduct.Id,
                Product = productMapper.ToModel(userProduct.Product),
                IsTracked = userProduct.IsTracked
            };
        }
    }
}
