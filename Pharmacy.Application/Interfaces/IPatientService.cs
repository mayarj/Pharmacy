using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IPatientService
    {
        public Task<IEnumerable<PatientDTO>> GetAllPatients();

        public Task<PatientDTO?> GetPatientById(int id);
        public Task<PatientDTO> CreatePatient(PatientDTO patient);
        public Task<PatientDTO?> UpdatePatient(PatientDTO patient);
        public Task DeletePatient(int id);
        public Task<IEnumerable<PrescriptionDTO>> GetPrescriptions(int patientId);

    }
}
