namespace SurveyBasket.api.Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "Invalide Email/password");

        public static readonly Error InvalidJwtToken =
    new("User.InvalidJwtToken", "Invalid Jwt token");

        public static readonly Error InvalidRefreshToken =
            new("User.InvalidRefreshToken", "Invalid refresh token");
    }
}
