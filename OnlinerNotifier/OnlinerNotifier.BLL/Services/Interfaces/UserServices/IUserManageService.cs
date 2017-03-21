namespace OnlinerNotifier.BLL.Services.Interfaces.UserServices
{
    public interface IUserManageService
    {
        int GetOrCreate(OAuth2.Models.UserInfo userInfo);
    }
}
