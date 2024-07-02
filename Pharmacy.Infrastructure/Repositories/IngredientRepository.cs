
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Pharmacy.Infrastructure.Models;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly PharmacyContext _pharmacyContext;

        public IngredientRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;

        }
        async Task<IngredientDTO> IIngredientRepository.CreateIngredient(IngredientDTO ingredient)
        {

            _pharmacyContext.Ingredients.Add(new Ingredient
            {
                Name = ingredient.Name,
                Description = ingredient.Description,

            });
            await _pharmacyContext.SaveChangesAsync();
            return ingredient;  
        }

        async Task IIngredientRepository.DeleteIngredient(int id)
        {
            var ingredient = _pharmacyContext.Ingredients.FirstOrDefault(p => p.Id == id);
            if (ingredient != null)
            {
                _pharmacyContext.Ingredients.Remove(ingredient);
                await _pharmacyContext.SaveChangesAsync();
            }
        }

        async Task <IEnumerable<IngredientDTO>> IIngredientRepository.GetAllIngredients()
        {
            return await  _pharmacyContext.Ingredients.Select(p => new IngredientDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description= p.Description,

            }).ToListAsync();
        }

        async Task <IngredientDTO?> IIngredientRepository.GetIngredientById(int id)
        {
            var ingredient = await _pharmacyContext.Ingredients.FirstOrDefaultAsync(p => p.Id == id);
            return ingredient != null ? new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description,

            } : null;
        }

        async Task <IngredientDTO> IIngredientRepository.UpdateIngredient(IngredientDTO ingredient)
        {
            var existingIngredient = await _pharmacyContext.Ingredients.FirstOrDefaultAsync(p => p.Id == ingredient.Id);
            if (existingIngredient != null)
            {
                existingIngredient.Name = ingredient.Name;
                existingIngredient.Description = ingredient.Description;

                await _pharmacyContext.SaveChangesAsync();
                ingredient.Name = existingIngredient.Name;
                ingredient.Id = existingIngredient.Id;
                return ingredient;
            }
            throw new InvalidOperationException();
            
        }
        async Task  <bool> IIngredientRepository.IsMedicineAssociatedWithIngredient(int activeSubstanceId)
        {
            return await _pharmacyContext.Medicines.AnyAsync(m => m.ActiveSubstanceId == activeSubstanceId);
        }
    }
}
