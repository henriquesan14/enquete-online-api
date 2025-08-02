using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Net;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenFacebook
{
    public class LoginAccessTokenFacebookCommandHandler(IFacebookAuthService facebookAuthService, IUnitOfWork unitOfWork, ITokenService tokenService,
        ICurrentUserService currentUserService) : ICommandHandler<LoginAccessTokenFacebookCommand, Result<AuthResponseViewModel>>
    {
        public async Task<Result<AuthResponseViewModel>> Handle(LoginAccessTokenFacebookCommand request, CancellationToken cancellationToken)
        {
            var userFacebook = await facebookAuthService.ObterUsuarioAsync(request.accessToken);
            if (userFacebook is null) return Result<AuthResponseViewModel>.Failure("Token inválido", HttpStatusCode.Unauthorized);

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

            var viewModel = new AuthResponseViewModel
            (
                User: user.ToViewModel(),
                authToken.AccessToken,
                authToken.RefreshToken
            );

            return Result<AuthResponseViewModel>.Success(viewModel);
        }
    }
}
