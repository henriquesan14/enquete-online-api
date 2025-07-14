using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.ExcluirEnquete
{
    public record ExcluirEnqueteCommand(Guid Id) : ICommand;
}
