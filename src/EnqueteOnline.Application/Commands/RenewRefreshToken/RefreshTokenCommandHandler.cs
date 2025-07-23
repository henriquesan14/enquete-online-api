using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Commands.RenewRefreshToken
{
    public class RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService, ICurrentUserService currentUserService) : ICommandHandler<RefreshTokenCommand, AuthResponseViewModel>
    {
        public async Task<AuthResponseViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = !string.IsNullOrEmpty(request.refreshToken)
                ? request.refreshToken
                : currentUserService.RefreshToken;

            var existingToken = await unitOfWork.RefreshTokens
                .GetSingleAsync(rt => rt.Token == refreshToken);

            if (existingToken is null || existingToken.IsExpired || existingToken.IsRevoked)
            {
                throw new InvalidRefreshTokenException("Sua sessão expirou");
            }

            var user = await unitOfWork.Usuarios.GetSingleAsync(u => u.Id == existingToken.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(existingToken.UserId.Value);
            }

            var authToken = tokenService.GenerateAccessToken(user);
            var newRefreshToken = RefreshToken.Create(RefreshTokenId.Of(Guid.NewGuid()), authToken.RefreshToken, user.Id, currentUserService.IpAddress!, DateTime.Now.AddDays(7));

            existingToken.Revoke(currentUserService.IpAddress!);
            existingToken.SetReplacedByToken(newRefreshToken.Token);

            await unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await unitOfWork.CompleteAsync();

            if (currentUserService.IsMobileClient()) {
                return new AuthResponseViewModel(
                    User: user.ToViewModel(),
                    AccessToken: authToken.AccessToken,
                    RefreshToken: newRefreshToken.Token
                );
            }
            currentUserService.SetRefreshTokenCookies(newRefreshToken.Token);
            return new AuthResponseViewModel(
                    User: user.ToViewModel(),
                    AccessToken: authToken.AccessToken,
                    RefreshToken: null
            );
        }     
    }
}
