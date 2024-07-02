using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IMedicineService
    {
        public Task<IEnumerable<MedicineDTO>> GetAllMedicines();
        public Task<MedicineDTO?> GetMedicineById(int id);
        public Task<MedicineDTO> CreateMedicine(MedicineDTO medicine);
        public Task<MedicineDTO> UpdateMedicine(MedicineDTO medicine);
        public Task DeleteMedicine(int id);

        //EntitieIngredient? GetMedicineActiveSubstanceById(int activeSubstanceId);
        public Task<IEnumerable<MedicineIngredientDTO>> GetIngredients(int medicineId);

        public Task<MedicineIngredientDTO?> GetMedicineIngredientDTO(int medicineId, int ingredientID);

        //public Task<bool> IsMedicineAssociatedWithPrescription(int medicineId);
        public Task<MedicineIngredientDTO> AddIngrediantToMedicen(MedicineIngredientDTO medicineIngredientDTO);
    }
}
