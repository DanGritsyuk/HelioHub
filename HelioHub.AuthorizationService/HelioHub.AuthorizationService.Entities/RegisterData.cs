
namespace HelioHub.AuthorizationService.Entities
{
    public class RegisterData
    {
        public RegisterData() { }

        //
        // Summary:
        //     The user's email address or another login data.
        public required string Login { get; init; }

        //
        // Summary:
        //     The user's password.
        public required string Password { get; init; }
    }
}