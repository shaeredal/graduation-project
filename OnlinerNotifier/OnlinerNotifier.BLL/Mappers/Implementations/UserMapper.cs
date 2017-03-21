using System;
using System.Linq;
using OAuth2.Models;
using OnlinerNotifier.BLL.Mappers.Interfaces;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Implementations
{
    public class UserMapper : IUserMapper
    {
        private readonly IUserProductsMapper userProductsMapper;

        public UserMapper(IUserProductsMapper userProductsMapper)
        {
            this.userProductsMapper = userProductsMapper;
        }

        public User ToDomain(UserInfo userInfo)
        {
            return new User()
            {
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                AvatarUri = userInfo.AvatarUri.Small,
                Email = userInfo.Email,
                SocialId = userInfo.Id,
                ProviderName = userInfo.ProviderName,
                NotificationTime = DateTime.Now
            };
        }

        public UserDataViewModel ToDataModel(User user)
        {
            return new UserDataViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarUri = user.AvatarUri,
                Email = user.Email,
                UserProducts = user.UserProducts.Select(up => userProductsMapper.ToModel(up)).ToList(),
                EnableNotifications = user.EnableNotifications
            };
        }
    }
}
