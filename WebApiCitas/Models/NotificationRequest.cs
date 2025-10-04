using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class NotificationRequest
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}