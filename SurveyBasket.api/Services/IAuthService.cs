
namespace SurveyBasket.api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken concellationToken =default);
    }
}
