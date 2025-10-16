namespace SurveyBasket.api.Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "Invalide Email/password",StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidJwtToken =
    new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

        public static readonly Error InvalidRefreshToken =
            new("User.InvalidRefreshToken", "Invalid refresh token",StatusCodes.Status401Unauthorized);
    }
}
