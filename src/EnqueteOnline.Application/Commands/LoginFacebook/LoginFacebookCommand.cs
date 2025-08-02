using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.LoginFacebook
{
    public record LoginFacebookCommand(string code) : ICommand<Result<string>>
    {
    }
}
