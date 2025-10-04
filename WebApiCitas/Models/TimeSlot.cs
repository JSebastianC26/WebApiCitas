using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class TimeSlot
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}