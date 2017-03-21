using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.UserProductServices
{
    public class UserProductAddService : IUserProductAddService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IProductMapper productMapper;

        private readonly IUserProductsMapper userProductsMapper;

        public UserProductAddService(IUnitOfWork unitOfWork, IProductMapper productMapper, IUserProductsMapper userProductsMapper)
        {
            this.unitOfWork = unitOfWork;
            this.productMapper = productMapper;
            this.userProductsMapper = userProductsMapper;
        }

        public bool AddToUser(ProductViewModel productModel, int userId)
        {
            var product = GetProduct(productModel) ?? CreateAndReturn(productModel);
            return AddToUser(product, userId);
        }

        private Product GetProduct(ProductViewModel productModel)
        {
            var product = unitOfWork.Products.GetAll().FirstOrDefault(prod => prod.OnlinerId == productModel.OnlinerId);
            return product;
        }

        private Product CreateAndReturn(ProductViewModel productModel)
        {
            var product = productMapper.ToDomain(productModel);
            unitOfWork.Products.Create(product);
            unitOfWork.Save();
            return product;
        }

        public bool AddToUser(Product product, int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            if (!user.UserProducts.Select(up => up.Product).Contains(product))
            {
                var userProduct = userProductsMapper.ToDomain(user, product);
                user.UserProducts.Add(userProduct);
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}
