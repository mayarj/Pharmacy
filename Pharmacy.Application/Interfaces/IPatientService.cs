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
        public Task<IEnumerable<EntitiePatient>> GetAllPatients();

        public Task<EntitiePatient?> GetPatientById(int id);
        public Task<EntitiePatient> CreatePatient(EntitiePatient patient);
        public Task<EntitiePatient?> UpdatePatient(EntitiePatient patient);
        public Task DeletePatient(int id);
        public Task<IEnumerable<EntitiePrescription>> GetPrescriptions(int patientId);

    }
}
