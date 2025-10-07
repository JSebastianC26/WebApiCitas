using System.Threading.Tasks;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de servicio para pagos.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Procesar un pago.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="amount"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<bool> ProcessPayment(int patientId, decimal amount, string method);
    }
}