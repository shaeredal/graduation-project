using System;
using System.Collections.Generic;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Services.Interfaces.NotificationDataSevices
{
    public interface IUserProductChangesService
    {
        List<NotificationProductChangesModel> GetProductChanges(User user, TimeSpan period);
    }
}