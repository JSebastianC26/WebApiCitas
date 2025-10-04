using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    [RoutePrefix("api/notifications")]
    public class NotificationsController : ApiController
    {
        private readonly INotificationService _notificationService;

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
