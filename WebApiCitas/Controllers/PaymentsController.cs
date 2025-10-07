using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Pagos
    /// </summary>
    [Authorize]
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _paymentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paymentService"></param>
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// POST /api/payments/confirm
        /// Confirma un pago realizado
        /// </summary>
        [HttpPost]
        [Route("confirm")]
        public async Task<IHttpActionResult> ConfirmPayment([FromBody] PaymentConfirmationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processed = await _paymentService.ProcessPayment(
                request.PatientId,
                request.Amount,
                request.PaymentMethod);

            if (processed)
                return Ok(new { success = true, message = "Pago confirmado" });

            return BadRequest("Error al procesar el pago");
        }

    }
}
