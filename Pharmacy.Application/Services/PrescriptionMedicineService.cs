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
    public class PrescriptionMedicineService : IPrescriptionMedicineService
    {
        private readonly IPrescriptionMedicineRepository _prescriptionMedicineRepository;

        public PrescriptionMedicineService(IPrescriptionMedicineRepository prescriptionMedicineRepository)
        {
            _prescriptionMedicineRepository = prescriptionMedicineRepository;
        }
        public async Task<PrescriptionMedicineDTO> CreatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine)
        {
            return await _prescriptionMedicineRepository.CreatePrescriptionMedicineDTO(prescriptionMedicine);
        }

        public async Task DeletePrescriptionMedicineDTO(int medicenId, int prescriptionID)
        {
            await _prescriptionMedicineRepository.DeletePrescriptionMedicineDTO(medicenId, prescriptionID);
        }

        public async Task<IEnumerable<PrescriptionMedicineDTO>> GetAllPrescriptionMedicineBymedcin(int medicenId)
        {
           return await _prescriptionMedicineRepository.GetAllPrescriptionMedicineBymedcin(medicenId);
        }

        public async Task<IEnumerable<PrescriptionMedicineDTO>> GetmedicineByPrescription(int prescriptionId)
        {
            return await _prescriptionMedicineRepository.GetmedicineByPrescription(prescriptionId);
        }

        public async Task<PrescriptionMedicineDTO?> GetPrescriptionMedicineById(int medicenId, int prescriptionID)
        {
            return await _prescriptionMedicineRepository.GetPrescriptionMedicineById(medicenId,prescriptionID);
        }
        public async Task<PrescriptionMedicineDTO> UpdatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine)
        {
            return await _prescriptionMedicineRepository.UpdatePrescriptionMedicineDTO(prescriptionMedicine);
        }

    }
}
