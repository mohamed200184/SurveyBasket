
using Microsoft.AspNetCore.Identity;
using SurveyBasket.api.Authentication;
using SurveyBasket.api.Entities;
using System.Security.Cryptography;

namespace SurveyBasket.api.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly int _refreshTokenExpiryInDays = 14;
        public async Task<AuthResponse?> GetTokenAsync(string email, string password, CancellationToken concellationToken = default)
        {
            var user=await _userManager.FindByEmailAsync(email);
            if (user is null)
                return null;
            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword)
                return null;
         var (token,experisIn) =  _jwtProvider.GenrateToken(user);
            var refershToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.Now.AddDays(_refreshTokenExpiryInDays);
        user.RefreshTokens
                .Add(
            new RefreshToken 
            { Token = refershToken, ExpiresOn = refreshTokenExpiration }
            );
            await _userManager.UpdateAsync(user);
            return new AuthResponse (user.Id, user.Email!,user.FirstName,user.LastName,token,experisIn, refershToken, refreshTokenExpiration);
        }




        public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshtoken, CancellationToken concellationToken = default)
        {
            var userId=_jwtProvider.ValidateToken(token);
            if (userId is null)
                return null;
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
                return null;
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshtoken && x.IsActive);
            if(userRefreshToken is null)
                return null;

            userRefreshToken.RevokedOn=DateTime.UtcNow;
            var (newtoken, experisIn) = _jwtProvider.GenrateToken(user);
            var newrefershToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.Now.AddDays(_refreshTokenExpiryInDays);
            user.RefreshTokens
                    .Add(
                new RefreshToken
                { Token = newrefershToken, ExpiresOn = refreshTokenExpiration }
                );
            await _userManager.UpdateAsync(user);
            return new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, newtoken, experisIn, newrefershToken, refreshTokenExpiration);

        }
        public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshtoken, CancellationToken concellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);
            if (userId is null)
                return false;
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;
            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshtoken && x.IsActive);
            if (userRefreshToken is null)
                return false;

            userRefreshToken.RevokedOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return true;
        }

        //helper methode 
        private static string GenerateRefreshToken()
        {

            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


    }

}
