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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task DeleteCategory(int id)
        {
            if (await _categoryRepository.IsMedicineAssociatedWithCategory(id))
            {
                throw new InvalidOperationException();
            }
            try { 
            await _categoryRepository.DeleteCategory(id); }
            catch (Exception ex) {
                throw ex;
            }

        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCatagories()
        {
            return await _categoryRepository.GetAllCatagories();
        }

        public async Task<IEnumerable<MedicineDTO>> GetAllMedicenBilongToCatagory(int id)
        {
            return await _categoryRepository.GetAllMedicenBilongToCatagory(id);
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<CategoryDTO?>  UpdateCategory(CategoryDTO category)
        {
            return await _categoryRepository.UpdateCategory(category);
        }
    }
}
