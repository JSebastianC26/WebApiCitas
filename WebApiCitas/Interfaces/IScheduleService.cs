using System;
using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Service interface for managing doctor schedules and appointments.
    /// </summary>
    public interface IScheduleService
    {
        /// <summary>
        /// Validar disponibilidad de un doctor en una fecha y hora específica.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<bool> CheckAvailability(int doctorId, DateTime date);

        /// <summary>
        /// Reservar un intervalo de tiempo para una cita.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<bool> ReserveTimeSlot(int doctorId, DateTime date);

        /// <summary>
        /// Reliminar un intervalo de tiempo reservado (en caso de cancelación).
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task ReleaseTimeSlot(int doctorId, DateTime date);

        /// <summary>
        /// lISTAR los intervalos de tiempo disponibles para un doctor en una fecha específica.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<System.Collections.Generic.List<TimeSlot>> GetAvailableSlots(int doctorId, DateTime date);
    }
}