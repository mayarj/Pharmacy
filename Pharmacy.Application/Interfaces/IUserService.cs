using Pharmacy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(UserDTO userDto, string password);
        Task<UserDTO> LoginAsync( string email, string password);
        Task LogoutAsync();
        Task<UserDTO> GetUserAsync(string email);
    }
}
