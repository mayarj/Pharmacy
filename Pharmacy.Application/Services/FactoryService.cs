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
    public class FactoryService : IFactoryService
    {
        private readonly IFactoryRepository _factoryRepository;

        public FactoryService(IFactoryRepository factoryRepository)
        {
            _factoryRepository = factoryRepository; 

        }
        public async Task<EntitieFactory> CreateFactory(EntitieFactory factory)
        {
            return await _factoryRepository.CreateFactory(factory);
        }

        public async Task DeleteFactory(int id)
        {
            if (await _factoryRepository.IsMedicineAssociatedWithFactory(id))
            {
                throw new InvalidOperationException();
            }
            await _factoryRepository.DeleteFactory(id);
        }

        public async Task<IEnumerable<EntitieFactory>> GetAllFactories()
        {
            return await _factoryRepository.GetAllFactories();
        }

        public async Task<IEnumerable<EntitieMedicine>> GetAllMedicendelongToFactory(int id)
        {
            return await _factoryRepository.GetAllMedicendelongToFactory(id);
        }

        public async Task<EntitieFactory?> GetFactoryById(int id)
        {
            return await _factoryRepository.GetFactoryById(id);
        }


        public async Task<EntitieFactory?> UpdateFactory(EntitieFactory factory)
        {
            return await _factoryRepository.UpdateFactory(factory);
        }
    }
}
