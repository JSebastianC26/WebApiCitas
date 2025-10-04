using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class ScheduleRequest
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}