using EnqueteOnline.Application.Contracts.Services.Response;

namespace EnqueteOnline.Application.Contracts.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleTokenResponse?> ObterTokenAsync(string code);
        Task<GoogleUserResponse?> ObterUsuarioAsync(string accessToken);
    }
}
