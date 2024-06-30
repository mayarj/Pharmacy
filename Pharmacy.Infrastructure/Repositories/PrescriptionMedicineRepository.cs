
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using Pharmacy.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class PrescriptionMedicineRepository : IPrescriptionMedicineRepository
    {
        private readonly PharmacyContext _pharmacyContext;

        public PrescriptionMedicineRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }

        async Task<PrescriptionMedicineDTO> IPrescriptionMedicineRepository.CreatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine)
        {
            if (prescriptionMedicine is null)
            {
                throw new ArgumentNullException(nameof(prescriptionMedicine));
            }
            var newPrescriptionMedicine = new PrescriptionMedicine
            {
                PrescriptionId = prescriptionMedicine.PrescriptionId,
                MedicineId = prescriptionMedicine.MedicineId,
                Count = prescriptionMedicine.Count,
            };

            await _pharmacyContext.PrescriptionMedicines.AddAsync(newPrescriptionMedicine);
            await _pharmacyContext.SaveChangesAsync();
            var prescriptionMedicineDto = new PrescriptionMedicineDTO()
            {
                Id = newPrescriptionMedicine.Id,
                PrescriptionId = newPrescriptionMedicine.PrescriptionId,
                MedicineId = newPrescriptionMedicine.MedicineId,
                Count = newPrescriptionMedicine.Count,
            };
            return prescriptionMedicineDto;
        }
        async Task<PrescriptionMedicineDTO> IPrescriptionMedicineRepository.UpdatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine)
        {
            if (prescriptionMedicine is null)
            {
                throw new ArgumentNullException(nameof(prescriptionMedicine));
            }
            var newPrescriptionMedicine = new PrescriptionMedicine
            {
                Id = prescriptionMedicine.Id,
                PrescriptionId = prescriptionMedicine.PrescriptionId,
                MedicineId = prescriptionMedicine.MedicineId,
                Count = prescriptionMedicine.Count,
            };

            _pharmacyContext.PrescriptionMedicines.Update(newPrescriptionMedicine);
            await _pharmacyContext.SaveChangesAsync();
            var prescriptionMedicineDto = new PrescriptionMedicineDTO()
            {
                Id = newPrescriptionMedicine.Id,
                PrescriptionId = newPrescriptionMedicine.PrescriptionId,
                MedicineId = newPrescriptionMedicine.MedicineId,
                Count = newPrescriptionMedicine.Count,
            };
            return prescriptionMedicineDto;
        }
        async Task IPrescriptionMedicineRepository.DeletePrescriptionMedicineDTO(int medicineId, int prescriptionId)
        {
            var prescriptionMedicine = await _pharmacyContext.PrescriptionMedicines
                .FirstOrDefaultAsync(pm => pm.MedicineId == medicineId && pm.PrescriptionId == prescriptionId);

            if (prescriptionMedicine != null)
            {
                _pharmacyContext.PrescriptionMedicines.Remove(prescriptionMedicine);
                await _pharmacyContext.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<PrescriptionMedicineDTO>> IPrescriptionMedicineRepository.GetAllPrescriptionMedicineBymedcin(int medicineId)
        {
            return await _pharmacyContext.PrescriptionMedicines
                .Where(pm => pm.MedicineId == medicineId)
                .Select(pm => new PrescriptionMedicineDTO
                {
                    PrescriptionId = pm.PrescriptionId,
                    MedicineId = pm.MedicineId,
                    Count = pm.Count,
                }).ToListAsync();
        }


        async Task<IEnumerable<PrescriptionMedicineDTO>> IPrescriptionMedicineRepository.GetmedicineByPrescription(int prescriptionId)
        {
            var query = await _pharmacyContext.PrescriptionMedicines
               .Where(pm => pm.PrescriptionId == prescriptionId)
               .Include(pm => pm.Medicine)
               .Include(pm => pm.Prescription)
               .Include(pm => pm.Prescription.Patient)
               .Select(pm => new PrescriptionMedicineDTO
               {
                   Count = pm.Count,
                   Id = pm.Id,
                   MedicineId = pm.MedicineId,
                   PrescriptionId = pm.PrescriptionId,
                   Medicine = new EntitieMedicine()
                   {
                       Id = pm.Medicine.Id,
                       Name = pm.Medicine.Name,
                       Description = pm.Medicine.Description,
                       CategoryId = pm.Medicine.CategoryId,
                       Dose = pm.Medicine.Dose,
                       ActiveSubstanceId = pm.Medicine.ActiveSubstanceId,
                       InStock = pm.Medicine.InStock,
                       FactoryId = pm.Medicine.FactoryId,
                       TradeName = pm.Medicine.TradeName,
                       ActiveSubstanceName = pm.Medicine.ActiveSubstance.Name,
                       CategoryName = pm.Medicine.Category.Name,
                       FactoryName = pm.Medicine.Factory.Name,
                   },
                   Prescription = new EntitiePrescription()
                   {
                       Id = pm.Prescription.Id,
                       Name = pm.Prescription.Name,
                       Note = pm.Prescription.Note,
                       PatientId = pm.PrescriptionId,
                       PatientName = pm.Prescription.Patient.FirstName + " " + pm.Prescription.Patient.LastName,
                   },

               }).ToListAsync();
            return query;
        }



        async Task<PrescriptionMedicineDTO?> IPrescriptionMedicineRepository.GetPrescriptionMedicineById(int medicineId, int prescriptionId)
        {
            var prescriptionMedicine = await _pharmacyContext.PrescriptionMedicines
                .FirstOrDefaultAsync(pm => pm.MedicineId == medicineId && pm.PrescriptionId == prescriptionId);

            if (prescriptionMedicine is not null)
            {
                return new PrescriptionMedicineDTO
                {
                    Id = prescriptionMedicine.Id,
                    PrescriptionId = prescriptionMedicine.PrescriptionId,
                    MedicineId = prescriptionMedicine.MedicineId,
                    Count = prescriptionMedicine.Count,
                };
            }

            return null;
        }
    }
}
