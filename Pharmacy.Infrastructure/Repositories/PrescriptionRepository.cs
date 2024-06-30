
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly PharmacyContext _pharmacyContext;

        public PrescriptionRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }
        async Task<EntitiePrescription> IPrescriptionRepository.CreatePrescription(EntitiePrescription prescription)
        {
            var p = new Prescription
            {
                PatientId = prescription.PatientId,
                Name = prescription.Name,
                Note = prescription.Note,

            };
            await _pharmacyContext.Prescriptions.AddAsync(p);
            await _pharmacyContext.SaveChangesAsync();
            var prescriptionDto = new EntitiePrescription()
            {
                Id = p.Id,
                PatientId = p.PatientId,
                Name = p.Name,
                Note = p.Note,
            };

            return prescriptionDto;
        }

        async Task IPrescriptionRepository.DeletePrescription(int id)
        {
            var prescription = await _pharmacyContext.Prescriptions.FirstOrDefaultAsync(p => p.Id == id);
            if (prescription is not null)
            {
                _pharmacyContext.Prescriptions.Remove(prescription);
                await _pharmacyContext.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<EntitiePrescription>> IPrescriptionRepository.GetAllPrescription()
        {
            return await _pharmacyContext.Prescriptions.Select(p => new EntitiePrescription
            {
                Id = p.Id,
                PatientId = p.PatientId,
                Name = p.Name,
                Note = p.Note,
                PatientName = p.Patient.FirstName + " " + p.Patient.LastName,
            }).ToListAsync();
        }



        async Task<EntitiePrescription?> IPrescriptionRepository.GetPrescriptionById(int id)
        {
            var prescription = await _pharmacyContext.Prescriptions.Include(p=>p.Patient).FirstOrDefaultAsync(p => p.Id == id);
            return prescription != null ? new EntitiePrescription
            {
                Id = prescription.Id,
                PatientId = prescription.PatientId,
                Name = prescription.Name,
                Note = prescription.Note,
                PatientName = prescription.Patient.FirstName + " " + prescription.Patient.LastName,
            } : null;
        }
        async Task<EntitiePrescription> IPrescriptionRepository.UpdatePrescription(EntitiePrescription prescription)
        {
            if (prescription is null)
            {
                throw new ArgumentNullException(nameof(prescription));
            }
            var existingPrescription = new Prescription()
            {
                Id = prescription.Id,
                PatientId = prescription.PatientId,
                Name = prescription.Name,
                Note = prescription.Note,
            };
            _pharmacyContext.Prescriptions.Update(existingPrescription);
            try
            {
                await _pharmacyContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Faild to update ");
            }
            return prescription;
        }
    }
}
