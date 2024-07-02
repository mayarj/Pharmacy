using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface ICategoryRepository
    {
        public Task <IEnumerable<CategoryDTO>> GetAllCatagories();
        public Task <CategoryDTO?> GetCategoryById(int id);
        public Task <CategoryDTO> CreateCategory(CategoryDTO category);
        public Task<CategoryDTO?> UpdateCategory(CategoryDTO category);
        public Task DeleteCategory(int id);

        public Task<IEnumerable<MedicineDTO>> GetAllMedicenBilongToCatagory(int id);
        public Task <bool> IsMedicineAssociatedWithCategory(int categoryId);

    }
}
