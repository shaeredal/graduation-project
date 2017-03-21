using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserProductServices;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations.UserProductServices
{
    public class UserProductGetService : IUserProductGetService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IProductMapper productMapper;

        public UserProductGetService(IUnitOfWork unitOfWork, IProductMapper productMapper)
        {
            this.unitOfWork = unitOfWork;
            this.productMapper = productMapper;
        }

        public List<ProductViewModel> GetUserProducts(int userId)
        {
            var user = unitOfWork.Users.Get(userId);
            return user.UserProducts.Select(up => up.Product).Select(prod => productMapper.ToModel(prod)).ToList();
        }
    }
}
