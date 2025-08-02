using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.ExcluirEnquete
{
    public record ExcluirEnqueteCommand(Guid Id) : ICommand<Result>;
}
