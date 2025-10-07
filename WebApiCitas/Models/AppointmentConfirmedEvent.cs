using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Confirmacion de una cita.
    /// </summary>
    public class AppointmentConfirmedEvent
    {
        /// <summary>
        /// Id de la cita.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Fecha de la confirmación.
        /// </summary>
        public DateTime ConfirmedAt { get; set; }
    }
}