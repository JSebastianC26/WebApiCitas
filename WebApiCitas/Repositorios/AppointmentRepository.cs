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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string _connectionString;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AppointmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Appointments 
                        (PatientId, DoctorId, AppointmentDate, Reason, Status, CreatedAt)
                    VALUES 
                        (@PatientId, @DoctorId, @AppointmentDate, @Reason, @Status, @CreatedAt);
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, appointment);
                appointment.Id = id;
                return appointment;
            }
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Appointments WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Appointment>(sql, new { Id = id });
            }
        }

        public async Task UpdateAppointment(Appointment appointment)
        {
            using (var connection = new MySqlConnection(_connectionString))
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
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Appointments WHERE PatientId = @PatientId";
                var result = await connection.QueryAsync<Appointment>(sql, new { PatientId = patientId });
                return result.ToList();
            }
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT * FROM Appointments 
                    WHERE DoctorId = @DoctorId 
                    AND DATE(AppointmentDate) = DATE(@Date)
                    AND Status != 'Cancelled'";

                var result = await connection.QueryAsync<Appointment>(
                    sql,
                    new { DoctorId = doctorId, Date = date });

                return result.ToList();
            }
        }
    }
}
