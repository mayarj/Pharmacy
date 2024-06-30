using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IIngredientService
    {
        public Task<IEnumerable<EntitieIngredient>> GetAllIngredients();
        public Task<EntitieIngredient?> GetIngredientById(int id);
        public Task<EntitieIngredient> CreateIngredient(EntitieIngredient ingredient);
        public Task<EntitieIngredient?> UpdateIngredient(EntitieIngredient ingredient);
        public Task DeleteIngredient(int id);

        //public Task<bool> IsMedicineAssociatedWithIngredient(int activeSubstanceId);
    }

}
