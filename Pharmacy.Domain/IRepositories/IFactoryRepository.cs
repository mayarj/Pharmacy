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
        public Task<IEnumerable<FactoryDTO>> GetAllFactories();
        public Task <FactoryDTO?> GetFactoryById(int id);
        public Task <FactoryDTO> CreateFactory(FactoryDTO factory);
        public Task<FactoryDTO?> UpdateFactory(FactoryDTO factory);
        public Task DeleteFactory(int id);
        public Task <IEnumerable<MedicineDTO>> GetAllMedicendelongToFactory(int id);

        public Task<bool> IsMedicineAssociatedWithFactory(int factoryId);
    }
}
