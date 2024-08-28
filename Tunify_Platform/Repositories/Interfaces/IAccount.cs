using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Tunify_Platform.Models.DTO;

namespace Tunify_Platform.Repositories.Interfaces
{
    public interface IAccount
    {
        // Register a new user
        Task<RegisterDto> Register(RegisterDto registeredUserDto, ModelStateDictionary modelState);

        // Authenticate a user
        Task<LoginDto> UserAuthentication(string username, string password);

        // Retrieve user profile
        Task<LoginDto> UserProfile(ClaimsPrincipal claimsPrincipal); // Updated to PascalCase
    }
}
