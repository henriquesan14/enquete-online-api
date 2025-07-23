using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenGoogle
{
    public class LoginAccessTokenGoogleCommandHandler(IGoogleAuthService googleAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        ICurrentUserService currentUserService) : ICommandHandler<LoginAccessTokenGoogleCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(LoginAccessTokenGoogleCommand request, CancellationToken cancellationToken)
        {
            var userGoogle = await googleAuthService.ObterUsuarioAsync(request.accessToken);
            if (userGoogle is null) throw new IntegrationException("Token inválido");

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

            return new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                authToken.AccessToken,
                authToken.RefreshToken
            );
        }
    }
}
