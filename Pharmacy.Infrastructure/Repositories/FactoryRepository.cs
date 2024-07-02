
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using Pharmacy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class FactoryRepository : IFactoryRepository
    {
        private readonly PharmacyContext _pharmacyContext;
        public  FactoryRepository(PharmacyContext pharmacyContext)
        {
            _pharmacyContext = pharmacyContext;
        }
        async Task <FactoryDTO> IFactoryRepository.CreateFactory(FactoryDTO factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            _pharmacyContext.Factories.Add(new Factory
            {
                Name = factory.Name,


            });
            await _pharmacyContext.SaveChangesAsync();
            return factory;
        }

        async Task IFactoryRepository.DeleteFactory(int id)
        {
            var factory = _pharmacyContext.Factories.FirstOrDefault(p => p.Id == id);
            if (factory != null)
            {
                _pharmacyContext.Factories.Remove(factory);
               await _pharmacyContext.SaveChangesAsync();
            }
        }

        async Task <IEnumerable<FactoryDTO>> IFactoryRepository.GetAllFactories()
        {
            return await _pharmacyContext.Factories.Select(p => new FactoryDTO
            {
                Id = p.Id,
                Name = p.Name,
            }).ToListAsync();
        }

        async Task <IEnumerable<MedicineDTO>> IFactoryRepository.GetAllMedicendelongToFactory(int id)
        {
 
                return await _pharmacyContext.Medicines.Where(p => p.FactoryId == id).Select(p => new MedicineDTO
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
                    CategoryName = p.Category.Name,
                    TradeName = p.TradeName
                }).ToListAsync();
        }

        async Task <FactoryDTO?> IFactoryRepository.GetFactoryById(int id)
        {
            var factory = await _pharmacyContext.Factories.FirstOrDefaultAsync(p => p.Id == id);
            return factory != null ? new FactoryDTO
            {
                Id = factory.Id,
                Name = factory.Name,

            } : null;
        }

        async Task <FactoryDTO> IFactoryRepository.UpdateFactory(FactoryDTO factory)
        {

            var existingFactory = _pharmacyContext.Factories.FirstOrDefault(p => p.Id == factory.Id);
            if (existingFactory != null)
            {
                existingFactory.Name = factory.Name;

                await _pharmacyContext.SaveChangesAsync();
                return factory;

            }
            return null;
        }

        async Task<bool>  IFactoryRepository.IsMedicineAssociatedWithFactory(int factoryId) {

            return await _pharmacyContext.Medicines.AnyAsync(m => m.FactoryId == factoryId);
        }
    }
}
