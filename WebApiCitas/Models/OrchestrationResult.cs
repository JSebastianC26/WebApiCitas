namespace WebApiCitas.Models
{
    /// <summary>
    /// OrchestrationResult representa el resultado de una orquestación de servicios para agendar una cita médica.
    /// </summary>
    public class OrchestrationResult
    {
        /// <summary>
        /// Indica si la orquestación fue exitosa.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Codigo de la cita médica creada.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Mensaje de error en caso de que la orquestación falle.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Confirmacuión de la cita médica creada.
        /// </summary>
        public AppointmentConfirmation Confirmation { get; set; }
    }
}