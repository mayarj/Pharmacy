using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.IRepositories;
using Pharmacy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class MedicineIngredientRepository : IMedicineIngredientRepository
    {
        private readonly PharmacyContext _context;

        public MedicineIngredientRepository(PharmacyContext context)
        {
            _context = context;
        }

        async Task  <IEnumerable<MedicineIngredientDTO>> IMedicineIngredientRepository.GetMedicineIngredientDTObymedicen(int medicineId)
        {
            return await _context.MedicineIngredients
                .Where(mi => mi.MedicineId == medicineId)
                .Select(mi => new MedicineIngredientDTO
                {
                    MedicineId = mi.MedicineId,
                    IngredientId = mi.IngredientId,
                    Ratio = mi.Ratio
                })
                .ToListAsync();
        }

        async Task <MedicineIngredientDTO> IMedicineIngredientRepository.Update(MedicineIngredientDTO medicineIngredientDTO)
        {
            var existingMedicineIngredient =await _context.MedicineIngredients
                .FirstOrDefaultAsync(mi => mi.MedicineId == medicineIngredientDTO.MedicineId && mi.IngredientId == medicineIngredientDTO.IngredientId);

            if (existingMedicineIngredient != null)
            {
                existingMedicineIngredient.Ratio = medicineIngredientDTO.Ratio;
                await _context.SaveChangesAsync();

                medicineIngredientDTO.Ratio = existingMedicineIngredient.Ratio;
                return medicineIngredientDTO;
            }
            throw new InvalidOperationException();

        }

       async Task  IMedicineIngredientRepository.Delete(int medicineId, int ingredientId)
        {
            var medicineIngredient = await _context.MedicineIngredients
                .FirstOrDefaultAsync(mi => mi.MedicineId == medicineId && mi.IngredientId == ingredientId);

            if (medicineIngredient != null)
            {
                _context.MedicineIngredients.Remove(medicineIngredient);
                await _context.SaveChangesAsync();
            }
        }
    }
}