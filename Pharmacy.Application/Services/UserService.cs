using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userReposetory;
        public UserService(IUserRepository userReposetory)
        {
            _userReposetory = userReposetory;
        }
        public async Task<UserDTO> GetUserAsync(string email)
        {
            return await _userReposetory.GetUserAsync(email);
        }

        public async Task<UserDTO> LoginAsync(string email, string password)
        {
            return await _userReposetory.LoginAsync(email, password);
        }

        public async Task LogoutAsync()
        {
            await _userReposetory.LogoutAsync();
        }

        public async Task<UserDTO> RegisterAsync(UserDTO userDto, string password)
        {
            return await _userReposetory.RegisterAsync(userDto, password);
        }
        public async Task<UserDTO> GetLoggedInUser(HttpContext httpContext)
        {
            return await _userReposetory.GetLoggedInUser(httpContext);
        }
    }
}
