using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApiCitas.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(int patientId, decimal amount, string method);
    }
}