using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class NotificationSentEvent
    {
        public int AppointmentId { get; set; }
        public string RecipientEmail { get; set; }
        public string NotificationType { get; set; }
        public bool Success { get; set; }
        public DateTime SentAt { get; set; }
    }
}