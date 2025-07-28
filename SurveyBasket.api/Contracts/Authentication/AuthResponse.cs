namespace SurveyBasket.api.Contracts.Authentication
{
    public record AuthResponse
    (
        string Id,// user 
        string FirstName,
        string LastName,
        string? Email,
        string Token,
        int ExpiresIn,
        string RefreshToken,
        DateTime REfreshTokenExpiration

    );
}
