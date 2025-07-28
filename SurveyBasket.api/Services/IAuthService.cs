
namespace SurveyBasket.api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken concellationToken =default);
        Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshtoken, CancellationToken concellationToken = default);
        Task<bool> RevokeRefreshTokenAsync(string token, string refreshtoken, CancellationToken concellationToken = default);

    }
}
