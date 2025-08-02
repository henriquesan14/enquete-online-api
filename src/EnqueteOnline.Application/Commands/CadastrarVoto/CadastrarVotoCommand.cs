using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.CadastrarVoto
{
    public record CadastrarVotoCommand(Guid EnqueteId, Guid OpcaoEnqueteId) : ICommand<Result<VotoViewModel>>;
}
