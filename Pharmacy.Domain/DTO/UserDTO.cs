using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Entities
{
    public class UserDTO
    {

        public string Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int? PatientId { get; set; }

        public string Email { get; set; }
        public string Password { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public bool Admin { get; set; }

    }
}
