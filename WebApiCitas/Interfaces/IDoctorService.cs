using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    public interface IDoctorService
    {
        Task<Doctor> GetDoctorById(int id);
        Task<System.Collections.Generic.List<Doctor>> GetDoctors(string specialty);
    }
}