using OnlinerNotifier.BLL.Models;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserDataService userService;

        public AccountController(IUserDataService userService)
        {
            this.userService = userService;
        }

        public UserDataViewModel GetData()
        {
            return userService.GetUserData(Principal.Id);
        }
    }
}
