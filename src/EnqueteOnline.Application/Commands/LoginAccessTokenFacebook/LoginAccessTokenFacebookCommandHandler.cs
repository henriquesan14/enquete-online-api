using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenFacebook
{
    public class LoginAccessTokenFacebookCommandHandler(IFacebookAuthService facebookAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        ICurrentUserService currentUserService) : ICommandHandler<LoginAccessTokenFacebookCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(LoginAccessTokenFacebookCommand request, CancellationToken cancellationToken)
        {
            var userFacebook = await facebookAuthService.ObterUsuarioAsync(request.accessToken);
            if (userFacebook is null) throw new IntegrationException("Token inválido");

            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.GoogleId == userFacebook.Id);

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

            return new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                authToken.AccessToken,
                authToken.RefreshToken
            );
        }
    }
}
