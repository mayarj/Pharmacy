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
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;   
        }

        public async Task<EntitiePrescription> CreatePrescription(EntitiePrescription Prescription)
        {
           return await _prescriptionRepository.CreatePrescription(Prescription);
        }

        public async Task DeletePrescription(int id)
        {
            await _prescriptionRepository.DeletePrescription(id);
        }

        public async Task<IEnumerable<EntitiePrescription>> GetAllPrescription()
        {
           return await _prescriptionRepository.GetAllPrescription();
        }

        public async Task<EntitiePrescription?> GetPrescriptionById(int id)
        {
           return await _prescriptionRepository.GetPrescriptionById(id);
        }

        public async Task<EntitiePrescription> UpdatePrescription(EntitiePrescription Prescription)
        {
            return await _prescriptionRepository.UpdatePrescription(Prescription);                       
        }
    }
}
