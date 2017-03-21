using System.Collections.Generic;
using OnlinerNotifier.DAL.Models;

namespace OnlinerNotifier.BLL.Models.NotificationModels
{
    public class NotificationProductChangesModel
    {
        public Product Product { get; set; }

        public List<ProductPriceChange> Changes { get; set; }

        public NotificationProductChangesModel()
        {
            Changes = new List<ProductPriceChange>();
        }
    }
}
