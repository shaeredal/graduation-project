using System.Collections.Generic;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Models.NotificationModels
{
    public class NotificationDataModel
    {
        public User User { get; set; }

        public List<NotificationProductChangesModel> Products { get; set; }

        public NotificationDataModel()
        {
            Products = new List<NotificationProductChangesModel>();
        }
    }
}
