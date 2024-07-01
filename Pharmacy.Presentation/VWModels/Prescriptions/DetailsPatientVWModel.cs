namespace Pharmacy.Web.VWModels.Prescriptions
{
    public class DetailsPrescriptionVWModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public string Patient { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public List<MedicineInfo> Medicines { get; set; } = new();
    }
}
