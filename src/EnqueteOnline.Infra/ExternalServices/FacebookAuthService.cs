using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Contracts.Services.Response;
using Microsoft.Extensions.Configuration;

namespace EnqueteOnline.Infra.ExternalServices
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly IFacebookApi _facebookApi;
        private readonly IConfiguration _config;

        public FacebookAuthService(IFacebookApi facebookApi, IConfiguration config)
        {
            _facebookApi = facebookApi;
            _config = config;
        }

        public async Task<FacebookTokenResponse?> ObterTokenAsync(string code)
        {
            var appId = _config["Facebook:AppId"]!;
            var appSecret = _config["Facebook:AppSecret"]!;
            var redirectUri = _config["Facebook:RedirectUri"]!;

            return await _facebookApi.ObterTokenAsync(appId, redirectUri, appSecret, code);
        }

        public async Task<FacebookUserResponse?> ObterUsuarioAsync(string accessToken)
        {
            return await _facebookApi.ObterUsuarioAsync("id,name,email,picture.width(200)", accessToken);
        }
    }
}
