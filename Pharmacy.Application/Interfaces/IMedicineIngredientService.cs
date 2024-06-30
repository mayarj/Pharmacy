using Pharmacy.Domain.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IMedicineIngredientService
    {
        public Task<IEnumerable<MedicineIngredientDTO>> GetMedicineIngredientDTObymedicen(int mrdicenId);
        public Task<MedicineIngredientDTO?> Update(MedicineIngredientDTO medicineIngredientDTO);
        public Task Delete(int medicenId, int ingrediantId);
    }
}
