using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenFacebook
{
    public record LoginAccessTokenFacebookCommand(string accessToken) : ICommand<Result<AuthResponseViewModel>>;
}
