using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Clase que representa una solicitud para crear una cita médica.
    /// </summary>
    public class AppointmentRequest
    {
        /// <summary>
        /// Identificacion del paciente que solicita la cita.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Codigo del médico con el que se desea agendar la cita.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Fecha de la cita
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Motivo de la cita.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Datos de pago para la cita.
        /// </summary>
        public PaymentInfo Payment { get; set; }
    }
}