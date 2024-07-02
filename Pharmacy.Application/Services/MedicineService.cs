using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository)
        {

            _medicineRepository = medicineRepository;

        }



        public async Task<MedicineDTO> CreateMedicine(MedicineDTO medicine)
        {
            if (medicine.ActiveSubstanceId is null)
            {
                throw new InvalidOperationException();
            }
            var newMedicine  = await _medicineRepository.CreateMedicine(medicine);

            await AddIngrediantToMedicen(new MedicineIngredientDTO()
            {
                IngredientId = (int)newMedicine.ActiveSubstanceId,
                MedicineId = newMedicine.Id,
                Ratio = 1,
            });
            return newMedicine;
        }

        public async Task DeleteMedicine(int id)
        {
            if (await _medicineRepository.IsMedicineAssociatedWithPrescription(id))
            {
                throw new InvalidOperationException();
            }
            await _medicineRepository.DeleteMedicine(id);
        }

        public async Task<IEnumerable<MedicineDTO>> GetAllMedicines()
        {
            return await _medicineRepository.GetAllMedicines();
        }

        public async Task<IEnumerable<MedicineIngredientDTO>> GetIngredients(int medicineId)
        {
            return await _medicineRepository.GetIngredients(medicineId);
        }

        public async Task<MedicineDTO?> GetMedicineById(int id)
        {
            return await _medicineRepository.GetMedicineById(id);
        }

        public async Task<MedicineIngredientDTO?> GetMedicineIngredientDTO(int medicineId, int ingredientID)
        {
            return await _medicineRepository.GetMedicineIngredientDTO(medicineId, ingredientID);
        }


        public async Task<MedicineDTO> UpdateMedicine(MedicineDTO medicine)
        {
            return await _medicineRepository.UpdateMedicine(medicine);
        }
        public async Task<MedicineIngredientDTO> AddIngrediantToMedicen(MedicineIngredientDTO medicineIngredientDTO)
        {
            if (await _medicineRepository.GetMedicineIngredientDTO(medicineIngredientDTO.MedicineId, medicineIngredientDTO.IngredientId) is not null)
            {
                throw new InvalidOperationException();
            }
            return await _medicineRepository.AddIngrediantToMedicen(medicineIngredientDTO);
        }
    }
}
