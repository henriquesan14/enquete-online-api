using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string? refreshToken) : ICommand;
}
