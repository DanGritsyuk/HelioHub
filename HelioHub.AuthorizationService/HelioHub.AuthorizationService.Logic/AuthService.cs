using HelioHub.AuthorizationService.Common.Models;
using HelioHub.AuthorizationService.Entities;
using HelioHub.AuthorizationService.Logic.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HelioHub.AuthorizationService.Logic
{
    public class AuthService : IAuthService
    {
        public Task<string?> LoginAsync(LoginData request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResult> RegisterAsync(RegisterData request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetJwtAsync(string login, string password)
        {
            // находим пользователя 
            //var user = await _userRepository.GetAsync(login);
            // если пользователь не найден, отправляем статусный код 401
            if (user is null
                || !_passwordService.VerifyPassword(password, user.Password)
                || user.IsBanned)
                throw new AuthenticationException($"User {login}: Incorrect login or password.");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user.Email"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.ExpiryMinutes)),
                signingCredentials: new SigningCredentials(_authOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
             );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt).ToString();
            _logger.Debug($"Generated token for user: {loginModel.Email}: {jwt}");

            // формируем ответ
            var response = new
            {
                access_token = encodedJwt,
                username = user.Email
            };

            return Results.Json(response);
        }
    }
}