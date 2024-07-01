namespace Pharmacy.Web.VWModels.Prescriptions
{
    public class CreatePrescriptionVWModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public int PatientId { get; set; }
    }
}
