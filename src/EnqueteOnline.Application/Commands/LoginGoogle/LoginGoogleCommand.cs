using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Commands.LoginGoogle
{
    public record LoginGoogleCommand(string code) : ICommand<AuthResponseViewModel>
    {
    }
}
