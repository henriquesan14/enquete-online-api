using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.CadastrarVoto
{
    public record CadastrarVotoCommand(Guid EnqueteId, Guid OpcaoEnqueteId) : ICommand<Guid>;
}
