using EnqueteOnline.Application.Contracts.Services.Response;
using Refit;

namespace EnqueteOnline.Infra.ExternalServices
{
    public interface IGoogleApi
    {
        [Post("/token")]
        [Headers("Content-Type: application/x-www-form-urlencoded")]
        Task<GoogleTokenResponse> ObterTokenAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, string> data);
    }
}
