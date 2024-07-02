using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.IRepositories;
using Pharmacy.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class UserRepository  : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        //private readonly IMapper _mapper;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_mapper = mapper;
        }

        public async Task<UserDTO> RegisterAsync(UserDTO userDto, string password)
        {
            var user = new User {
                Id = userDto.Email,
                UserName = userDto.UserName,
                Email = userDto.Email,
                PatientId = userDto.PatientId,
            };
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return new UserDTO {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                PatientId = userDto.PatientId,
                Admin = false,
            };
        }

        public async Task<UserDTO> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid login attempt.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            return new UserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Admin = false,
                PatientId = user.PatientId,
            };
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Id = user.Id,
                Admin = false,
            };
        }
        public async Task<UserDTO> GetLoggedInUser(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(principal: httpContext.User);
            return new UserDTO
            {
                Admin = false,
                Id = user.Id,
                Email = user.Email,
                PatientId  = user.PatientId,
                UserName = user.UserName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
        }
    }
}
