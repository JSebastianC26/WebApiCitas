using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> GetPatientById(int id);
        Task<Patient> CreatePatient(CreatePatientRequest request);
    }
}