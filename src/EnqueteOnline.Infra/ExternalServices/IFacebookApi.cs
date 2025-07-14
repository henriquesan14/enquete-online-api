using EnqueteOnline.Application.Contracts.Services.Response;
using Refit;

namespace EnqueteOnline.Infra.ExternalServices
{
    public interface IFacebookApi
    {
        [Get("/v19.0/oauth/access_token")]
        Task<FacebookTokenResponse> ObterTokenAsync(
        [Query] string client_id,
        [Query] string redirect_uri,
        [Query] string client_secret,
        [Query] string code
    );

        [Get("/me")]
        Task<FacebookUserResponse> ObterUsuarioAsync(
            [Query] string fields,
            [Query] string access_token
        );
    }
}
