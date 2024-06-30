using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class MedicineIngredientService : IMedicineIngredientService
    {
        private readonly IMedicineIngredientRepository _medicineIngredientRepository;
        public MedicineIngredientService(IMedicineIngredientRepository medicineIngredientRepository)
        {
            _medicineIngredientRepository = medicineIngredientRepository;
        }
        public async Task Delete(int medicenId, int ingrediantId)
        {
            await _medicineIngredientRepository.Delete(medicenId, ingrediantId);
        }

        public async Task<IEnumerable<MedicineIngredientDTO>> GetMedicineIngredientDTObymedicen(int mrdicenId)
        {
            return await _medicineIngredientRepository.GetMedicineIngredientDTObymedicen(mrdicenId);
        }

        public async Task<MedicineIngredientDTO?> Update(MedicineIngredientDTO medicineIngredientDTO)
        {
            return await _medicineIngredientRepository.Update(medicineIngredientDTO);
        }
    }
}
