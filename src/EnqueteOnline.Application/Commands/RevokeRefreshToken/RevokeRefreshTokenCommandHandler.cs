using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Abstractions;
using System.Net;

namespace EnqueteOnline.Application.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<RevokeRefreshTokenCommand, Result>
    {
        public async Task<Result> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = !string.IsNullOrEmpty(request.refreshToken)
                ? request.refreshToken
                : currentUserService.RefreshToken;
            if (string.IsNullOrEmpty(refreshToken))
                return Result.Failure("refreshToken cookie not found", HttpStatusCode.Unauthorized);

            if (string.IsNullOrEmpty(request.refreshToken))
                currentUserService.RemoveRefreshTokenCookies();

            var token = await unitOfWork.RefreshTokens.GetSingleAsync(r => r.Token == refreshToken);
            if (token == null)
                return Result.Failure("refreshToken cookie not found", HttpStatusCode.Unauthorized);

            token.Revoke(currentUserService.IpAddress!);
            await unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}
