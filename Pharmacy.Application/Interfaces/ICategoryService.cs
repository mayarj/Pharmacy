using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<EntitieCategory>> GetAllCatagories();
        public Task<EntitieCategory?> GetCategoryById(int id);
        public Task<EntitieCategory> CreateCategory(EntitieCategory category);
        public Task<EntitieCategory?> UpdateCategory(EntitieCategory category);
        public Task DeleteCategory(int id);

        public Task<IEnumerable<EntitieMedicine>> GetAllMedicenBilongToCatagory(int id);

        //public bool IsMedicineAssociatedWithCategory(int categoryId);
    }
}
