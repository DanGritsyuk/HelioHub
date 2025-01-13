using HelioHub.AuthorizationService.Common.Models;
using HelioHub.AuthorizationService.Entities;

namespace HelioHub.AuthorizationService.Logic.Contracts
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync(RegisterData request);
        Task<string?> LoginAsync(LoginData request);
    }
}