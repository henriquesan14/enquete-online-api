using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Commands.CadastrarEnquete
{
    public record CadastrarEnqueteCommand(string Titulo, string Descricao, DateTime Encerramento, List<string> Opcoes) : ICommand<Result<EnqueteViewModel>>;
}
