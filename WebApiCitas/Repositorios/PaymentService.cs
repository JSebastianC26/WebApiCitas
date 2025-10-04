using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Repositorios
{
    public class PaymentService : IPaymentService
    {
        private readonly string _connectionString;

        public PaymentService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> ProcessPayment(int patientId, decimal amount, string method)
        {
            try
            {
                // Aquí integrarías con un gateway de pagos real
                // (Stripe, PayPal, MercadoPago, etc.)

                using (var connection = new SqlConnection(_connectionString))
                {
                    var sql = @"
                        INSERT INTO Payments 
                            (PatientId, Amount, PaymentMethod, Status, ProcessedAt)
                        VALUES 
                            (@PatientId, @Amount, @PaymentMethod, 'Completed', GETDATE())";

                    await connection.ExecuteAsync(sql, new
                    {
                        PatientId = patientId,
                        Amount = amount,
                        PaymentMethod = method
                    });

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}