using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.LoginGoogle
{
    public record LoginGoogleCommand(string code) : ICommand<Result<string>>
    {
    }
}
