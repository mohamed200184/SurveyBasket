namespace SurveyBasket.api.Authentication
{
    public interface IJwtProvider
    {
        (string token ,int expiresIn) GenrateToken(ApplicationUser user);
        string? ValidateToken(string token);
    }
}
