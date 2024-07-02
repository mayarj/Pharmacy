using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class PatientService : IPatientService
    {

        private readonly IPatientRepository _patientRepository;



        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;

        }

        public async Task<IEnumerable<PatientDTO>> GetAllPatients()
        {
            return await _patientRepository.GetAllPatients();
        }
        public async Task<PatientDTO?> GetPatientById(int id)
        {
            return await _patientRepository.GetPatientById(id);
        }

        public async Task<PatientDTO> CreatePatient(PatientDTO patient)
        {
            return await _patientRepository.CreatePatient(patient);
        }

        public async Task<PatientDTO?> UpdatePatient(PatientDTO patient)
        {
            return await _patientRepository.UpdatePatient(patient);
        }

        public async Task DeletePatient(int id)
        {
            await _patientRepository.DeletePatient(id);
        }

        public async Task<IEnumerable<PrescriptionDTO>> GetPrescriptions(int patientId)
        {
            return await _patientRepository.GetPrescriptions(patientId);
        }


    }
}
