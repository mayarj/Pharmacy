using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Web.VWModels.MedicineIngredient
{
    public class AddIngredientToMedicineVWModel
    {
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public decimal Ratio { get; set; }
    }
}
