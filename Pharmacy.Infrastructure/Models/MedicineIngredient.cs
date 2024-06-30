using System;
using System.Collections.Generic;

namespace Pharmacy.Infrastructure.Models
{
    public partial class MedicineIngredient
    {
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public decimal Ratio { get; set; }
        public int Id { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
    }
}
