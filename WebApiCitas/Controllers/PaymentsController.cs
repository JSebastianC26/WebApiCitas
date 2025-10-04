using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _paymentService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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
