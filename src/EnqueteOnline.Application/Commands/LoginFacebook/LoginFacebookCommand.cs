using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Commands.LoginFacebook
{
    public record LoginFacebookCommand(string code) : ICommand<AuthResponseViewModel>
    {
    }
}
