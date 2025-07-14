using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Commands.RenewRefreshToken
{
    public record RefreshTokenCommand : ICommand<UsuarioViewModel>;
}
