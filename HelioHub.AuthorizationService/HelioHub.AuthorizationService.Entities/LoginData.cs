namespace HelioHub.AuthorizationService.Entities
{
    public class LoginData
    {
        public LoginData() { }

        //
        // Summary:
        //     The user's email address or another login data.
        public required string Email { get; init; }
        //
        // Summary:
        //     The user's password.
        public required string Password { get; init; }
        //
        // Summary:
        //     The optional two-factor authenticator code. This may be required for users who
        //     have enabled two-factor authentication. This is not required if a Microsoft.AspNetCore.Identity.Data.LoginRequest.TwoFactorRecoveryCode
        //     is sent.
        public string? TwoFactorCode { get; init; }
        //
        // Summary:
        //     An optional two-factor recovery code from Microsoft.AspNetCore.Identity.Data.TwoFactorResponse.RecoveryCodes.
        //     This is required for users who have enabled two-factor authentication but lost
        //     access to their Microsoft.AspNetCore.Identity.Data.LoginRequest.TwoFactorCode.
        public string? TwoFactorRecoveryCode { get; init; }
    }
}