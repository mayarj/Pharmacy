using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int PatientId { get;  set; }
        public string Name { get;  set; } = string.Empty;
        public string PatientName { get;  set; } = string.Empty;
        public string? Note { get; set; }

    }
}
