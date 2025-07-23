using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.LoginGoogle
{
    public record LoginGoogleCommand(string code) : ICommand<string>
    {
    }
}
