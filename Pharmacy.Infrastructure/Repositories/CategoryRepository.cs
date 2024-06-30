using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Infrastructure.Models;

using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PharmacyContext _pharmacyContext;
        public CategoryRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }

        async Task<EntitieCategory> ICategoryRepository.CreateCategory(EntitieCategory category)
        {

            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _pharmacyContext.Categories.Add(new Category
            {
                Name = category.Name,

            });
            await _pharmacyContext.SaveChangesAsync();
            return category;
        }

        async Task ICategoryRepository.DeleteCategory(int id)
        {
            var category = _pharmacyContext.Categories.FirstOrDefault(p => p.Id == id);
            if (category != null)
            {
                try
                {
                    _pharmacyContext.Categories.Remove(category);
                    await _pharmacyContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        async Task<IEnumerable<EntitieCategory>> ICategoryRepository.GetAllCatagories()
        {
            return await _pharmacyContext.Categories.Select(p => new EntitieCategory
            {
                Id = p.Id,
                Name = p.Name,
            }).ToListAsync();
        }
        async Task<IEnumerable<EntitieMedicine>> ICategoryRepository.GetAllMedicenBilongToCatagory(int id)
        {
            return await _pharmacyContext.Medicines.Where(p => p.CategoryId == id).Select(p => new EntitieMedicine
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Dose = p.Dose,
                ActiveSubstanceId = p.ActiveSubstanceId,
                InStock = p.InStock,
                FactoryId = p.FactoryId,
                ActiveSubstanceName = p.ActiveSubstance.Name,
                FactoryName = p.Factory.Name,
                TradeName = p.TradeName
            }).ToListAsync();
        }

        async Task<EntitieCategory?> ICategoryRepository.GetCategoryById(int id)
        {
            var category = await _pharmacyContext.Categories.FirstOrDefaultAsync(p => p.Id == id);
            return category is not null ? new EntitieCategory
            {
                Id = category.Id,
                Name = category.Name,

            } : null;
        }

        async Task<EntitieCategory?> ICategoryRepository.UpdateCategory(EntitieCategory category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            var existingCategory = await _pharmacyContext.Categories.FirstOrDefaultAsync(p => p.Id == category.Id);
            if (existingCategory is not null)
            {
                existingCategory.Name = category.Name;

                await _pharmacyContext.SaveChangesAsync();
                EntitieCategory entitieCategory = new EntitieCategory
                {
                    Id = existingCategory.Id,
                    Name = existingCategory.Name,
                };
                return entitieCategory;

            }

            return null;


        }


        async Task<bool> ICategoryRepository.IsMedicineAssociatedWithCategory(int categoryId)
        {
            return await _pharmacyContext.Medicines.AnyAsync(m => m.CategoryId == categoryId);
        }
    }
}
