using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.RenewRefreshToken
{
    public record RefreshTokenCommand(string refreshToken) : ICommand<Result<AuthResponseViewModel>>;
}
