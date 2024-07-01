namespace Pharmacy.Web.VWModels.Medicines
{
    public class EditMedicineVWModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal Dose { get; set; }
        public int ActiveSubstanceId { get; set; }
        public int InStock { get; set; }
        public int FactoryId { get; set; }
        public string TradeName { get; set; } = null!;
    }
}
