using System;
using System.Threading.Tasks;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de servicio de notificaciones.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Enviar confirnmación de cita.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="doctorName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task SendAppointmentConfirmation(string email, string doctorName, DateTime date);

        /// <summary>
        /// Enviar cancelacion de cita
        /// </summary>
        /// <param name="email"></param>
        /// <param name="doctorName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task SendCancellationNotification(string email, string doctorName, DateTime date);

        /// <summary>
        /// Enviar notificación personalizada.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendCustomNotification(string email, string subject, string message);
    }
}