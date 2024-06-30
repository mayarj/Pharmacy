using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class EntitieMedicine
    {
        public int Id { get;  set; }
        public string Name { get;  set; } = string.Empty!;
        public string? Description { get;  set; } = string.Empty!;
        public int CategoryId { get;  set; }
        public decimal Dose { get; set; }
        public int? ActiveSubstanceId { get;  set; }
        public int InStock { get; set; }
        public int FactoryId { get;  set; }
        public string TradeName { get;  set; } = string.Empty;

        public string FactoryName { get; set; } = string.Empty;

        public string ActiveSubstanceName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

    }
}
