using OnlinerNotifier.BLL.Models.TrackingModels;

namespace OnlinerNotifier.BLL.Services.Interfaces
{
    public interface ITrackingService
    {
        bool ChangeStatus(TrackingModel trackingModel);
    }
}
