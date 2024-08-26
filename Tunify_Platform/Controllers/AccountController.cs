using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tunify_Platform.Models.DTO;
using Tunify_Platform.Repositories.Interfaces;
using Tunify_Platform.Repositories.Services;

namespace Tunify_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccount accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerdUserDto)
        {
            _logger.LogInformation("Attempting to register user: {UserName}", registerdUserDto.UserName);

            var user = await _accountService.Register(registerdUserDto, this.ModelState);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("User registered successfully: {UserName}", registerdUserDto.UserName);
                return Ok(user);
            }

            _logger.LogWarning("Registration failed for user: {UserName}", registerdUserDto.UserName);
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginDto>> Login(LoginDto loginDto)
        {
            _logger.LogInformation("Attempting to log in user: {Username}", loginDto.Username);

            var user = await _accountService.UserAuthentication(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                _logger.LogWarning("Login failed for user: {Username}", loginDto.Username);
                return Unauthorized();
            }

            _logger.LogInformation("User logged in successfully: {Username}", loginDto.Username);
            return Ok(user);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("Attempting to log out user.");

            await HttpContext.SignOutAsync();
            _logger.LogInformation("User logged out successfully.");

            return Ok(new { message = "Logged out successfully." });
        }

        //[Authorize(Roles = "Admin")] // only logged in users can have access to the profile
        [HttpGet("Profile")]
        public async Task<ActionResult<LoginDto>> Profile()
        {
            return await _accountService.userProfile(User);
        }

    }
}
