using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.LoginFacebook
{
    public record LoginFacebookCommand(string code) : ICommand<string>
    {
    }
}
