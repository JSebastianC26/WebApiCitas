using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
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

                using (var connection = new MySqlConnection(_connectionString))
                {
                    var sql = @"
                        INSERT INTO Payments 
                            (PatientId, Amount, PaymentMethod, Status, ProcessedAt)
                        VALUES 
                            (@PatientId, @Amount, @PaymentMethod, 'Completed', NOW())";

                    await connection.ExecuteAsync(sql, new
                    {
                        PatientId = patientId,
                        Amount = amount,
                        PaymentMethod = method
                    });

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error con log4net
                // Log.Error("Error en ProcessPayment", ex);
                return false;
            }
        }
    }
}
