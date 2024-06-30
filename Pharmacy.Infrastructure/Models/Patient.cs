using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Prescriptions = new HashSet<Prescription>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal PhoneNumber { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
