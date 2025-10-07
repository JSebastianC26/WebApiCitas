using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Notificaciones
    /// </summary>
    [Authorize]
    [RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="notificationService"></param>
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// POST /api/notifications/send
        /// Envía una notificación manual
        /// </summary>
        [HttpPost]
        [Route("send")]
        public async Task<IHttpActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _notificationService.SendCustomNotification(
                request.RecipientEmail,
                request.Subject,
                request.Message);

            return Ok(new { message = "Notificación enviada" });
        }
    }
}
