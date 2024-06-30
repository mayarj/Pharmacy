using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            PrescriptionMedicines = new HashSet<PrescriptionMedicine>();
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = null!;
        public string? Note { get; set; }

        public virtual Patient Patient { get; set; } = null!;
        public virtual ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }
}
