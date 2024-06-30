using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Aggregation
{
    public class MedicineIngredientDTO
    {
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public decimal Ratio { get; set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;

    }
}
