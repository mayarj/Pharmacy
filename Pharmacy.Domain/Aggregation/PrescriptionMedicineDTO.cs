using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Domain.Aggregation
{
    public class PrescriptionMedicineDTO
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int Count { get; set; }
        public int Id { get; set; }
        public PrescriptionDTO Prescription { get; set; }
        public MedicineDTO Medicine { get; set; }
    }
}
