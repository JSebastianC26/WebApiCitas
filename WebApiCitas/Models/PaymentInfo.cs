namespace WebApiCitas.Models
{
    /// <summary>
    /// Datos de pago para una cita médica.
    /// </summary>
    public class PaymentInfo
    {
        /// <summary>
        /// Precio de la cita médica.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Metodo de pago utilizado (e.g., Tarjeta de crédito, PayPal).
        /// </summary>
        public string PaymentMethod { get; set; }
    }
}