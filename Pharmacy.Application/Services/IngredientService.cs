﻿using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }
        public async Task<EntitieIngredient> CreateIngredient(EntitieIngredient ingredient)
        {
            return await _ingredientRepository.CreateIngredient(ingredient);
        }

        public async Task DeleteIngredient(int id)
        {
            if (await _ingredientRepository.IsMedicineAssociatedWithIngredient(id))
            {
                throw new InvalidOperationException();
            }

            await _ingredientRepository.DeleteIngredient(id);
        }

        public async Task<IEnumerable<EntitieIngredient>> GetAllIngredients()
        {
            return await _ingredientRepository.GetAllIngredients();
        }

        public async Task<EntitieIngredient?> GetIngredientById(int id)
        {
            return await _ingredientRepository.GetIngredientById(id);
        }

        public async Task<EntitieIngredient?> UpdateIngredient(EntitieIngredient ingredient)
        {
            return await _ingredientRepository.UpdateIngredient(ingredient);
        }
    }
}
