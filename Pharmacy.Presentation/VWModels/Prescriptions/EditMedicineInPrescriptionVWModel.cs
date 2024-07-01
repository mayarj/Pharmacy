using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Web.VWModels.Prescriptions
{
    public class EditMedicineInPrescriptionVWModel
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public int Count { get; set; }

    }
}
