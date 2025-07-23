using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace EnqueteOnline.Application.Commands.LoginGoogle
{
    public class LoginGoogleCommandHandler(IGoogleAuthService googleAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        IConfiguration configuration, ICurrentUserService currentUserService) : ICommandHandler<LoginGoogleCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            var token = await googleAuthService.ObterTokenAsync(request.code);
            if (token is null) throw new IntegrationException("Erro ao obter token do Google.");

            var userGoogle = await googleAuthService.ObterUsuarioAsync(token.AccessToken);
            if (userGoogle is null) throw new IntegrationException("Erro ao obter usuário do Google.");

            
            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.GoogleId == userGoogle.Sub);

            if (user is null)
            {
                user = Usuario.CreateGoogle(
                    nome: userGoogle.Name,
                    email: Email.Of(userGoogle.Email),
                    avatarUrl: userGoogle.Picture,
                    googleId: userGoogle.Sub
                );
                await unitOfWork.Usuarios.AddAsync(user);
            }

            var authToken = tokenService.GenerateAccessToken(user);

            var refreshToken = RefreshToken.Create(
                id: RefreshTokenId.Of(Guid.NewGuid()),
                token: authToken.RefreshToken,
                userId: UsuarioId.Of(user.Id.Value),
                expiresAt: authToken.RefreshTokenExpiresAt,
                createdByIp: currentUserService.IpAddress!
                );
            await unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await unitOfWork.CompleteAsync();

            currentUserService.SetCookieTokens(authToken.AccessToken, authToken.RefreshToken);

            return new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                RedirectAppUrl: GenerateRedirectUrl(user, request.isMobile)
            );
        }
        private string GenerateRedirectUrl(Usuario user, bool isMobile)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(user.ToViewModel(), options);

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            if(isMobile) return $"{configuration["Google:RedirectAppUrl"]}?data={base64}";
            return $"{configuration["Google:RedirectAppUrl"]}#data={base64}";
        }
    }
}
