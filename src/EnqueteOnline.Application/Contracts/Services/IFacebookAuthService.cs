using EnqueteOnline.Application.Contracts.Services.Response;

namespace EnqueteOnline.Application.Contracts.Services
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenResponse?> ObterTokenAsync(string code);
        Task<FacebookUserResponse?> ObterUsuarioAsync(string accessToken);
    }
}
