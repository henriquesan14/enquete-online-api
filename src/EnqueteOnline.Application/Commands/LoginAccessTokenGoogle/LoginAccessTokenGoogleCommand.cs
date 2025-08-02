using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenGoogle
{
    public record LoginAccessTokenGoogleCommand(string accessToken) : ICommand<Result<AuthResponseViewModel>>;
}
