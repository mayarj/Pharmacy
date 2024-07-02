using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface IPrescriptionRepository
    {
        public Task<IEnumerable<PrescriptionDTO>> GetAllPrescription();
        public Task <PrescriptionDTO?> GetPrescriptionById(int id);
        public Task <PrescriptionDTO> CreatePrescription(PrescriptionDTO Prescription);
        public Task <PrescriptionDTO>  UpdatePrescription(PrescriptionDTO Prescription);
        public Task DeletePrescription(int id);
    }
}
