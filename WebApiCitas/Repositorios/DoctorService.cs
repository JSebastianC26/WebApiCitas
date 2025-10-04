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
    public class DoctorService : IDoctorService
    {
        private readonly string _connectionString;

        public DoctorService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Doctors WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Doctor>(sql, new { Id = id });
            }
        }

        public async Task<List<Doctor>> GetDoctors(string specialty)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Doctors";

                if (!string.IsNullOrEmpty(specialty))
                {
                    sql += " WHERE Specialty = @Specialty";
                    var result = await connection.QueryAsync<Doctor>(sql, new { Specialty = specialty });
                    return result.ToList();
                }

                var allDoctors = await connection.QueryAsync<Doctor>(sql);
                return allDoctors.ToList();
            }
        }
    }
}