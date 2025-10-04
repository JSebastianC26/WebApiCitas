using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Repositorios
{
    public class PatientService : IPatientService
    {
        private readonly string _connectionString;

        public PatientService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Patients WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Patient>(sql, new { Id = id });
            }
        }

        public async Task<Patient> CreatePatient(CreatePatientRequest request)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Patients (Name, Email, Phone, DateOfBirth, CreatedAt)
                    VALUES (@Name, @Email, @Phone, @DateOfBirth, NOW());
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, request);

                return new Patient
                {
                    Id = id,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };
            }
        }
    }
}
