namespace OnlinerNotifier.BLL.Services.Interfaces.UserProductServices
{
    public interface IUserProductRemoveService
    {
        bool RemoveFromUser(int userId, int productId);
    }
}
