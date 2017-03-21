using OAuth2.Models;
using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User ToDomain(UserInfo userInfo);
        UserDataViewModel ToDataModel(User user);
    }
}