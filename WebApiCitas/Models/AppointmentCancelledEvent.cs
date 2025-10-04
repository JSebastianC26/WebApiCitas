using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class AppointmentCancelledEvent
    {
        public int AppointmentId { get; set; }
        public int CancelledBy { get; set; }
        public string Reason { get; set; }
        public DateTime CancelledAt { get; set; }
    }
}