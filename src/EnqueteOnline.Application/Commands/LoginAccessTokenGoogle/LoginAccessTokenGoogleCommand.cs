using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenGoogle
{
    public record LoginAccessTokenGoogleCommand(string accessToken) : ICommand<AuthResponseViewModel>;
}
