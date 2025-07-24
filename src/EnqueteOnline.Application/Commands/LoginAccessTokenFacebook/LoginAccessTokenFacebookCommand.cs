using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Commands.LoginAccessTokenFacebook
{
    public record LoginAccessTokenFacebookCommand(string accessToken) : ICommand<AuthResponseViewModel>;
}
