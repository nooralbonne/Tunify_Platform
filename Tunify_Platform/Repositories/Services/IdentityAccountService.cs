using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Tunify_Platform.Models;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;

namespace Tunify_Platform.Repositories.Services
{
    public class IdentityAccountService : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _jwtTokenService;

        public IdentityAccountService(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<RegisterDto> Register(RegisterDto registeredUserDto, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registeredUserDto.UserName,
                Email = registeredUserDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registeredUserDto.Password);

            if (result.Succeeded)
            {
                return new RegisterDto()
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            foreach (var error in result.Errors)
            {
                string errorCode = error.Code.Contains("Password") ? "Password" :
                                   error.Code.Contains("Email") ? "Email" :
                                   error.Code.Contains("Username") ? "Username" : "General";

                modelState.AddModelError(errorCode, error.Description);
            }

            return null;
        }

        public async Task<LoginDto> UserAuthentication(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null; // User not found
            }

            bool passValidation = await _userManager.CheckPasswordAsync(user, password);

            if (passValidation)
            {
                return new LoginDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await _jwtTokenService.GenerateToken(user, TimeSpan.FromMinutes(7))
                };
            }

            return null;
        }

        public async Task<LoginDto> userProfile(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            if (user == null)
            {
                return null;
            }

            return new LoginDto()
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await _jwtTokenService.GenerateToken(user, TimeSpan.FromMinutes(7)) // just for development purposes
            };
        }
    }
}
