
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;

using Pharmacy.Infrastructure.Models;
namespace Pharmacy.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository

    {

        private readonly PharmacyContext _pharmacyContext;

        public  PatientRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }

        async Task<IEnumerable<EntitiePatient> >IPatientRepository.GetAllPatients()
        {
            return await _pharmacyContext.Patients.Select(p => new EntitiePatient
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                PhoneNumber = p.PhoneNumber,
            
            }).ToListAsync();
            
        }

        async Task<EntitiePatient?> IPatientRepository.GetPatientById(int id)
        {
            var patient = await _pharmacyContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            return patient != null ? new EntitiePatient
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber
            } : null;
        }

        async Task<EntitiePatient> IPatientRepository.CreatePatient(EntitiePatient patient)
        {
            var newPatient = new Patient
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber
            };

            await _pharmacyContext.Patients.AddAsync(newPatient);
            await _pharmacyContext.SaveChangesAsync();
            var patientDto = new EntitiePatient()
            {
                Id = newPatient.Id,
                FirstName = newPatient.FirstName,
                LastName = newPatient.LastName,
                Address = newPatient.Address,
                PhoneNumber = newPatient.PhoneNumber
            };
            return patientDto;
        }

        async Task<EntitiePatient?> IPatientRepository.UpdatePatient(EntitiePatient patient)
        {
            if (patient is null)
            {
                throw new ArgumentNullException(nameof(patient));
            }
            var existingPatient = await _pharmacyContext.Patients.FirstOrDefaultAsync(p => p.Id == patient.Id);
            if (existingPatient is not null)
            {
                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.Address = patient.Address;
                existingPatient.PhoneNumber = patient.PhoneNumber;
                await _pharmacyContext.SaveChangesAsync();
            }
            return patient;
        }

        async Task  IPatientRepository.DeletePatient(int id)
        {
            var patient = await _pharmacyContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient is not null)
            {
                 _pharmacyContext.Patients.Remove(patient);
                await _pharmacyContext.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<EntitiePrescription>>   IPatientRepository.GetPrescriptions( int patientId)
        {
            return await _pharmacyContext.Prescriptions.Where(p => p.PatientId == patientId).Select(p => new EntitiePrescription
            {
                Id = p.Id,
                PatientId = p.PatientId,
                Name = p.Name,
                Note = p.Note,
            }).ToListAsync();
        }

        
    }
}
