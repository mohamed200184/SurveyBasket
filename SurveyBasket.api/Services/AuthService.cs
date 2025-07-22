
using Microsoft.AspNetCore.Identity;
using SurveyBasket.api.Authentication;

namespace SurveyBasket.api.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken concellationToken = default)
        {
            var user=await _userManager.FindByEmailAsync(email);
            if (user is null)
                return null;
            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                return null;
         //   var token = await _userManager.GenerateUserTokenAsync(user, "Email", concellationToken);
         var (token,experisIn) =  _jwtProvider.GenrateToken(user);
            return new AuthResponse (user.Id, user.Email!,user.FirstName,user.LastName,token,experisIn);
        }
    }
}
