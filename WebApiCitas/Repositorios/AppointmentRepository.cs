using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Repositorios
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string _connectionString;

        public AppointmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Appointments 
                        (PatientId, DoctorId, AppointmentDate, Reason, Status, CreatedAt)
                    VALUES 
                        (@PatientId, @DoctorId, @AppointmentDate, @Reason, @Status, @CreatedAt);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                var id = await connection.ExecuteScalarAsync<int>(sql, appointment);
                appointment.Id = id;
                return appointment;
            }
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Appointments WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Appointment>(sql, new { Id = id });
            }
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE Appointments 
                    SET Status = @Status, UpdatedAt = @UpdatedAt
                    WHERE Id = @Id";

                await connection.ExecuteAsync(sql, appointment);
            }
        }

        public async Task<List<Appointment>> GetAppointmentsByPatient(int patientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Appointments WHERE PatientId = @PatientId";
                var result = await connection.QueryAsync<Appointment>(sql, new { PatientId = patientId });
                return result.ToList();
            }
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT * FROM Appointments 
                    WHERE DoctorId = @DoctorId 
                    AND CAST(AppointmentDate AS DATE) = CAST(@Date AS DATE)
                    AND Status != 'Cancelled'";

                var result = await connection.QueryAsync<Appointment>(
                    sql,
                    new { DoctorId = doctorId, Date = date });

                return result.ToList();
            }
        }
    }
}