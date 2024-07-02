using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface IIngredientRepository
    {
        public Task<IEnumerable<IngredientDTO>> GetAllIngredients();
        public Task<IngredientDTO?> GetIngredientById(int id);
        public Task<IngredientDTO> CreateIngredient(IngredientDTO ingredient);
        public Task<IngredientDTO?> UpdateIngredient(IngredientDTO ingredient);
        public Task DeleteIngredient(int id);

        public Task<bool> IsMedicineAssociatedWithIngredient(int activeSubstanceId);

    }
}
