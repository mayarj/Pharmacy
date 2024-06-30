using Pharmacy.Domain.Aggregation;
using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface IPrescriptionMedicineRepository
    {
        public Task<IEnumerable<PrescriptionMedicineDTO>> GetAllPrescriptionMedicineBymedcin(int medicenId);
        public Task <IEnumerable<PrescriptionMedicineDTO>> GetmedicineByPrescription(int prescriptionId);
        public Task <PrescriptionMedicineDTO?> GetPrescriptionMedicineById( int medicenId, int prescriptionID);
        public Task <PrescriptionMedicineDTO>  CreatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine);
        public Task<PrescriptionMedicineDTO> UpdatePrescriptionMedicineDTO(PrescriptionMedicineDTO prescriptionMedicine);

        public Task  DeletePrescriptionMedicineDTO(int medicenId ,int prescriptionID);
    }
}
