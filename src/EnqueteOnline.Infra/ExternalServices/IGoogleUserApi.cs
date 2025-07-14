using EnqueteOnline.Application.Contracts.Services.Response;
using Refit;

namespace EnqueteOnline.Infra.ExternalServices
{
    public interface IGoogleUserApi
    {
        [Get("/userinfo")]
        Task<GoogleUserResponse> ObterUsuarioAsync([Query] string access_token);
    }
}
