
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
    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyContext _pharmacyContext;

        public MedicineRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }
        async Task<MedicineDTO> IMedicineRepository.CreateMedicine(MedicineDTO medicine)
        {
            Medicine addMedicen = new Medicine
            {
                Name = medicine.Name,
                Description = medicine.Description,
                CategoryId = medicine.CategoryId,
                Dose = medicine.Dose,
                ActiveSubstanceId = (int)medicine.ActiveSubstanceId,
                InStock = medicine.InStock,
                FactoryId = medicine.FactoryId,
                TradeName = medicine.TradeName

            };
            await _pharmacyContext.Medicines.AddAsync(addMedicen);
            await _pharmacyContext.SaveChangesAsync();
            medicine.Id = addMedicen.Id;
            return medicine;
        }

        async Task IMedicineRepository.DeleteMedicine(int id)
        {
            var medicine = _pharmacyContext.Medicines.FirstOrDefault(p => p.Id == id);
            if (medicine is not null)
            {
                _pharmacyContext.Medicines.Remove(medicine);
                await _pharmacyContext.SaveChangesAsync();
                return;
            }
            throw new Exception(" no such a medicine ");
        }

        async Task<IEnumerable<MedicineDTO>> IMedicineRepository.GetAllMedicines()
        {
            return await _pharmacyContext.Medicines.Select(p => new MedicineDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Dose = p.Dose,
                ActiveSubstanceId = p.ActiveSubstanceId,
                InStock = p.InStock,
                FactoryId = p.FactoryId,
                FactoryName = p.Factory.Name,
                CategoryName = p.Category.Name,
                ActiveSubstanceName = p.ActiveSubstance.Name,
                TradeName = p.TradeName

            }).ToListAsync();
        }

        async Task<IEnumerable<MedicineIngredientDTO>> IMedicineRepository.GetIngredients(int medicineId)
        {
            return await _pharmacyContext.MedicineIngredients
              .Where(pm => pm.MedicineId == medicineId)
              .Include(i => i.Ingredient)
              .Select(pm => new MedicineIngredientDTO
              {
                  IngredientId = pm.IngredientId,
                  MedicineId = pm.MedicineId,
                  Id = pm.Id,
                  Name = pm.Ingredient.Name,
                  Ratio = pm.Ratio,
              }).ToListAsync();
        }
        async Task<MedicineIngredientDTO?> IMedicineRepository.GetMedicineIngredientDTO(int medicineId, int ingredientId)
        {
            var medicineIngredient = await _pharmacyContext.MedicineIngredients
                .FirstOrDefaultAsync(mi => mi.MedicineId == medicineId && mi.IngredientId == ingredientId);

            if (medicineIngredient != null)
            {
                return new MedicineIngredientDTO
                {
                    MedicineId = medicineIngredient.MedicineId,
                    IngredientId = medicineIngredient.IngredientId,
                    Ratio = medicineIngredient.Ratio
                };
            }

            return null;
        }

        /*public EntitieIngredient? GetMedicineActiveSubstanceById(int activeSubstanceId)
        {
            var ingredient = _pharmacyContext.Ingredients.FirstOrDefault(p => p.Id == activeSubstanceId);
            return ingredient != null ? new EntitieIngredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description,

            } : null;
        }*/

        async Task<MedicineDTO?> IMedicineRepository.GetMedicineById(int id)
        {
            var medicine = _pharmacyContext.Medicines
        .Where(p => p.Id == id)
        .Select(p => new MedicineDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CategoryId = p.CategoryId,
            Dose = p.Dose,
            ActiveSubstanceId = p.ActiveSubstanceId,
            InStock = p.InStock,
            FactoryId = p.FactoryId,
            TradeName = p.TradeName,
            CategoryName = p.Category.Name,
            FactoryName = p.Factory.Name,
            ActiveSubstanceName = p.ActiveSubstance.Name,

        })
        .FirstOrDefault();

            return medicine;
        }

        async Task<MedicineDTO> IMedicineRepository.UpdateMedicine(MedicineDTO medicine)
        {
            if(medicine.ActiveSubstanceId is null)
            {
                throw new InvalidOperationException();
            }
             _pharmacyContext.Medicines.Update(new Medicine()
            {
                
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                CategoryId = medicine.CategoryId,
                Dose = medicine.Dose,
                ActiveSubstanceId = (int)medicine.ActiveSubstanceId,
                InStock = medicine.InStock,
                FactoryId = medicine.FactoryId,
                TradeName = medicine.TradeName,
            });
            await _pharmacyContext.SaveChangesAsync();

            return medicine;
        }
        async Task<bool> IMedicineRepository.IsMedicineAssociatedWithPrescription(int medicineId)
        {
            return await _pharmacyContext.PrescriptionMedicines.AnyAsync(m => m.MedicineId == medicineId);
        }

        async Task<MedicineIngredientDTO> IMedicineRepository.AddIngrediantToMedicen(MedicineIngredientDTO medicineIngredientDTO)
        {
            var medicineIngredient = new MedicineIngredient()
            {
                IngredientId = medicineIngredientDTO.IngredientId,
                MedicineId = medicineIngredientDTO.MedicineId,
                Ratio = medicineIngredientDTO.Ratio
            };
            await _pharmacyContext.MedicineIngredients.AddAsync(medicineIngredient);
            await _pharmacyContext.SaveChangesAsync();
            return new MedicineIngredientDTO()
            {
                Id = medicineIngredient.Id,
                IngredientId = medicineIngredient.IngredientId,
                MedicineId = medicineIngredient.MedicineId,
                Ratio = medicineIngredient.Ratio
            };


        }
    }
}
