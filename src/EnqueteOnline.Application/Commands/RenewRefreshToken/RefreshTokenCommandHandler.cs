using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Net;

namespace EnqueteOnline.Application.Commands.RenewRefreshToken
{
    public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, ICurrentUserService currentUserService) : 
        ICommandHandler<RefreshTokenCommand, Result<AuthResponseViewModel>>
    {
        public async Task<Result<AuthResponseViewModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = !string.IsNullOrEmpty(request.refreshToken)
                ? request.refreshToken
                : currentUserService.RefreshToken;

            var existingToken = await unitOfWork.RefreshTokens
                .GetSingleAsync(rt => rt.Token == refreshToken);

            if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
            {
                return Result<AuthResponseViewModel>.Failure("Sua sessão expirou", HttpStatusCode.Unauthorized);
            }

            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.Id == existingToken.UserId);
            if (user is null)
            {
                return Result<AuthResponseViewModel>.Failure("Usuário não encontrado", HttpStatusCode.NotFound);
            }

            var authToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = RefreshToken.Create(RefreshTokenId.Of(Guid.NewGuid()), authToken.RefreshToken, user.Id, currentUserService.IpAddress!, DateTime.Now.AddDays(7));

            existingToken.Revoke(currentUserService.IpAddress!);
            existingToken.SetReplacedByToken(newRefreshToken.Token);

            await unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await unitOfWork.CompleteAsync();
            AuthResponseViewModel authViewModel = null!;
            if (currentUserService.IsMobileClient()) {
                authViewModel = new AuthResponseViewModel(
                    User: user.ToViewModel(),
                    AccessToken: authToken.AccessToken,
                    RefreshToken: newRefreshToken.Token
                );

                return Result<AuthResponseViewModel>.Success(authViewModel);
            }
            currentUserService.SetRefreshTokenCookies(newRefreshToken.Token);
            authViewModel = new AuthResponseViewModel(
                    User: user.ToViewModel(),
                    AccessToken: authToken.AccessToken,
                    RefreshToken: null
            );

            return Result<AuthResponseViewModel>.Success(authViewModel);
        }     
    }
}
