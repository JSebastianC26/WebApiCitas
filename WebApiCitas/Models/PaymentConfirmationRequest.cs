using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCitas.Models
{
    public class PaymentConfirmationRequest
    {
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
    }
}