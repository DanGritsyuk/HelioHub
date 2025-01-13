namespace HelioHub.AuthorizationService.Common.Models
{
    /// <summary>
    /// Represents the result of an authentication or registration operation.
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A message providing additional details about the result of the operation.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// A JWT token returned in case of successful authentication.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// A refresh token used to obtain a new JWT token when the current one expires.
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// A collection of error messages in case the operation fails.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Creates a successful AuthResult with a token and refresh token.
        /// </summary>
        public static AuthResult SuccessResult(string token, string refreshToken)
        {
            return new AuthResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken,
                Message = "Operation completed successfully."
            };
        }

        /// <summary>
        /// Creates a failed AuthResult with an error message.
        /// </summary>
        public static AuthResult FailedResult(string message, List<string>? errors = null)
        {
            return new AuthResult
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
