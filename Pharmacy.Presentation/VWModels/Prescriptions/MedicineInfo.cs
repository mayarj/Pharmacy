namespace Pharmacy.Web.VWModels.Prescriptions
{
    public class MedicineInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string TradeName { get; set; } = string.Empty!;
        public string Category { get; set; } = string.Empty!;
        public int Count { get; set; }
        public decimal Dose { get; set; }
    }
}
