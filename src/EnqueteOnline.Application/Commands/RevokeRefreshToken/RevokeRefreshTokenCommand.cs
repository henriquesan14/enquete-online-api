using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string? refreshToken) : ICommand<Result>;
}
