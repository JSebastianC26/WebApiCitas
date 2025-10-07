using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Cancelacion ed una cita
    /// </summary>
    public class AppointmentCancelledEvent
    {
        /// <summary>
        /// Id de la cita.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Identificador del usuario que cancela la cita.
        /// </summary>
        public int CancelledBy { get; set; }

        /// <summary>
        /// Motivo de la cancelación.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Fecha de la cancelación.
        /// </summary>
        public DateTime CancelledAt { get; set; }
    }
}