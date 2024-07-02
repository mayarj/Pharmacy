using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.IRepositories
{
    public interface IUserRepository
    {
        public Task<UserDTO> RegisterAsync(UserDTO userDto, string password);
        public Task<UserDTO> LoginAsync(string email, string password);
        public Task LogoutAsync();
        public Task<UserDTO> GetUserAsync(string email);
    }
}
