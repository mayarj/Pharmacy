using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Infrastructure.Models
{
    public partial class User : IdentityUser<string>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
