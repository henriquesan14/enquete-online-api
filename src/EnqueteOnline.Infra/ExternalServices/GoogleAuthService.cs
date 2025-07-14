using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Contracts.Services.Response;
using Microsoft.Extensions.Configuration;

namespace EnqueteOnline.Infra.ExternalServices
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IGoogleApi _googleApi;
        private readonly IGoogleUserApi _googleUserApi;
        private readonly IConfiguration _config;

        public GoogleAuthService(IGoogleApi googleApi, IGoogleUserApi googleUserApi, IConfiguration config)
        {
            _googleApi = googleApi;
            _googleUserApi = googleUserApi;
            _config = config;
        }

        public async Task<GoogleTokenResponse?> ObterTokenAsync(string code)
        {
            var clientId = _config["Google:ClientId"]!;
            var clientSecret = _config["Google:ClientSecret"]!;
            var redirectUri = _config["Google:RedirectUri"]!;

            var data = new Dictionary<string, string>
        {
            { "code", code },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "redirect_uri", redirectUri },
            { "grant_type", "authorization_code" }
        };

            return await _googleApi.ObterTokenAsync(data);
        }

        public async Task<GoogleUserResponse?> ObterUsuarioAsync(string accessToken)
        {
            return await _googleUserApi.ObterUsuarioAsync(accessToken);
        }
    }
}
