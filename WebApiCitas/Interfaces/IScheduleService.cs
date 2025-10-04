using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> CheckAvailability(int doctorId, DateTime date);
        Task<bool> ReserveTimeSlot(int doctorId, DateTime date);
        Task ReleaseTimeSlot(int doctorId, DateTime date);
        Task<System.Collections.Generic.List<TimeSlot>> GetAvailableSlots(int doctorId, DateTime date);
    }
}