using OnlinerNotifier.BLL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.UserServices
{
    public interface IUserDataService
    {
        UserDataViewModel GetUserData(int id);
    }
}