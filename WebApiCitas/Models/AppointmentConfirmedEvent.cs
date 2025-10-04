using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class AppointmentConfirmedEvent
    {
        public int AppointmentId { get; set; }
        public DateTime ConfirmedAt { get; set; }
    }
}