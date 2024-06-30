using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IPrescriptionService
    {
        public Task<IEnumerable<EntitiePrescription>> GetAllPrescription();
        public Task<EntitiePrescription?> GetPrescriptionById(int id);
        public Task<EntitiePrescription> CreatePrescription(EntitiePrescription Prescription);
        public Task<EntitiePrescription> UpdatePrescription(EntitiePrescription Prescription);
        public Task DeletePrescription(int id);
    }
}
