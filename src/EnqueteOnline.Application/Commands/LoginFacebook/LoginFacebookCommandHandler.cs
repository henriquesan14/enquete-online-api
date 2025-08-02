using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EnqueteOnline.Application.Commands.LoginFacebook
{
    public class LoginFacebookCommandHandler(IFacebookAuthService facebookAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        IConfiguration configuration, ICurrentUserService currentUserService) : ICommandHandler<LoginFacebookCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(LoginFacebookCommand request, CancellationToken cancellationToken)
        {
            var token = await facebookAuthService.ObterTokenAsync(request.code);
            if (token is null) return Result<string>.Failure("Erro ao obter token do Facebook.", HttpStatusCode.Unauthorized);

            var userFacebook = await facebookAuthService.ObterUsuarioAsync(token.AccessToken);
            if (userFacebook is null) return Result<string>.Failure("Erro ao obter usuário do Facebook.", HttpStatusCode.Unauthorized);


            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.FacebookId == userFacebook.Id);

            if (user is null)
            {
                user = Usuario.CreateFacebook(
                    nome: userFacebook.Name,
                    email: Email.Of(userFacebook.Email),
                    avatarUrl: userFacebook.Picture.Data.Url,
                    facebookId: userFacebook.Id
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


            var authResponse = new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                AccessToken: authToken.AccessToken,
                null
            );

            currentUserService.SetRefreshTokenCookies(refreshToken.Token);

            var redirectUrl = GenerateRedirectUrl(authResponse);

            return Result<string>.Success(redirectUrl);
        }
        private string GenerateRedirectUrl(AuthResponseViewModel authResponse)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(authResponse, options);

            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            return $"{configuration["Facebook:RedirectAppUrl"]}#data={base64}";
        }
    }
}
