namespace Pharmacy.Web.VWModels.Patients
{
    public class CreatePatientVWModel
    {
        public string FirstName { get; set; } = string.Empty!;
        public string LastName { get; set; } = string.Empty!;
        public string Address { get; set; } = string.Empty!;
        public decimal PhoneNumber { get; set; }
    }
}
