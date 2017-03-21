using System.Web.Http;
using OnlinerNotifier.BLL.Models.TrackingModels;
using OnlinerNotifier.BLL.Services.Interfaces;
using OnlinerNotifier.Filters;
using OnlinerNotifier.ToastNotifier;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class TrackingController : ApiControllerBase
    {
        private readonly ITrackingService trackingService;
        private readonly IToastNotifier toastNotifier;

        public TrackingController(ITrackingService trackingService, IToastNotifier toastNotifier)
        {
            this.trackingService = trackingService;
            this.toastNotifier = toastNotifier;
        }

        public IHttpActionResult Post([FromBody] TrackingModel trackingModel)
        {
            if (trackingService.ChangeStatus(trackingModel))
            {
                SendMessage(trackingModel.Status);
                return Ok();
            };
            return NotFound();
        }

        private void SendMessage(bool status)
        {
            string statusString = "";
            if (!status)
            {
                statusString = "not ";
            }
            toastNotifier.Send(Request, $"Product is {statusString}tracked.");
        }
    }
}
