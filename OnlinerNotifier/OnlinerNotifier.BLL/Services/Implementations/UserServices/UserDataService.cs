using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations.UserServices
{
    public class UserDataService : IUserDataService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserMapper userMapper;

        public UserDataService(IUnitOfWork unitOfWork, IUserMapper userMapper)
        {
            this.unitOfWork = unitOfWork;
            this.userMapper = userMapper;
        }

        public UserDataViewModel GetUserData(int id)
        {
            var user = unitOfWork.Users.Get(id);
            if (user == null)
            {
                return null;
            }
            return userMapper.ToDataModel(user);
        }
    }
}
