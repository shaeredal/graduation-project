using System.Linq;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.UserServices
{
    public class UserManageService : IUserManageService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IUserMapper userMapper;

        public UserManageService(IUnitOfWork unitOfWork, IUserMapper userMapper)
        {
            this.unitOfWork = unitOfWork;
            this.userMapper = userMapper;
        }

        public int GetOrCreate(OAuth2.Models.UserInfo userInfo)
        {
            var user = GetUser(userInfo.ProviderName, userInfo.Id) ?? CreateUser(userInfo);
            return user.Id;
        }

        private User GetUser(string providerName, string socialId)
        {
            return unitOfWork.Users.GetAll().Where(usr => usr.ProviderName == providerName)
                .FirstOrDefault(usr => usr.SocialId == socialId);
        }

        private User CreateUser(OAuth2.Models.UserInfo userInfo)
        {
            var user = userMapper.ToDomain(userInfo);
            unitOfWork.Users.Create(user);
            unitOfWork.Save();
            return user;
        }
    }
}
