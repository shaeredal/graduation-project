using System.Web.Http;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services.Interfaces.UserServices;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class NotificationController : ApiControllerBase
    {
        private readonly IUserNotificationsService notificationsService;

        public NotificationController(IUserNotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]NotificationParametersModel parameters)
        {
            if (notificationsService.SetNotificationParameters(Principal.Id, parameters))
            {
                return Ok();
            }
            return Conflict();
        }

        public IHttpActionResult Delete()
        {
            if (notificationsService.DisableNotifications(Principal.Id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
