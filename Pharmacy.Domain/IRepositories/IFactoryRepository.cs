using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface IFactoryRepository
    {
        public Task<IEnumerable<EntitieFactory>> GetAllFactories();
        public Task <EntitieFactory?> GetFactoryById(int id);
        public Task <EntitieFactory> CreateFactory(EntitieFactory factory);
        public Task<EntitieFactory?> UpdateFactory(EntitieFactory factory);
        public Task DeleteFactory(int id);
        public Task <IEnumerable<EntitieMedicine>> GetAllMedicendelongToFactory(int id);

        public Task<bool> IsMedicineAssociatedWithFactory(int factoryId);
    }
}
