using HelioHub.AuthorizationService.Common.Models;
using HelioHub.AuthorizationService.Entities;
using HelioHub.AuthorizationService.Logic.Contracts;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelioHub.AuthorizationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // добавить автомапер
            RegisterData requestData = new RegisterData { Login = request.Email, Password = request.Password };

            AuthResult result = await _authService.RegisterAsync(requestData);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            LoginData loginData = new LoginData
            {
                Email = request.Email,
                Password = request.Password,
                TwoFactorCode = request.TwoFactorCode,
                TwoFactorRecoveryCode = request.TwoFactorRecoveryCode
            };

            var token = await _authService.LoginAsync(loginData);
            return token != null ? Ok(token) : Unauthorized();
        }
    }
}