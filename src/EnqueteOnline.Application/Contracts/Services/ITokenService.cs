using EnqueteOnline.Application.Contracts.Services.Response;
using EnqueteOnline.Domain.Entities;

namespace EnqueteOnline.Application.Contracts.Services
{
    public interface ITokenService
    {
        AuthTokenResult GenerateAccessToken(Usuario user);
    }
}
