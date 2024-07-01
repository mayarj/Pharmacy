using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Web.VWModels.Medicines
{
    public class EditIngredientInMedicineVWModel
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int MedicineId { get; set; }
        public decimal Ratio  { get; set; }
        public string IngredientName { get; set; } = string.Empty;
    }
}
