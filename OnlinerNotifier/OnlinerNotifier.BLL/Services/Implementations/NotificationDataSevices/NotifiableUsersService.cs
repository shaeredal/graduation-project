using System.Collections.Generic;
using System.Linq;
using OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices;
using OnlinerNotifier.BLL.Validators;
using OnlinerNotifier.DAL;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Implementations.NotificationDataSevices
{
    public class NotifiableUsersService : INotifiableUsersProvider
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IEmailValidator emailValidator;

        public NotifiableUsersService(IUnitOfWork unitOfWork, IEmailValidator emailValidator)
        {
            this.unitOfWork = unitOfWork;
            this.emailValidator = emailValidator;
        }

        public List<User> GetNotifiableUsers()
        {
            return unitOfWork.Users.GetAllDeep()
                .Where(u => u.EnableNotifications)
                .Where(u => emailValidator.IsValid(u.Email))
                .ToList();
        }
    }
}
