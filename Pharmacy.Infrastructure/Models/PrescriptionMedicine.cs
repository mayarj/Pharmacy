using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models
{
    public partial class PrescriptionMedicine
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int Count { get; set; }
        public int Id { get; set; }

        public virtual Medicine Medicine { get; set; } = null!;
        public virtual Prescription Prescription { get; set; } = null!;
    }
}
