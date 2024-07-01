namespace Pharmacy.Web.VWModels.Patients
{
    public class PrescriptionInfo
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
    }
}
