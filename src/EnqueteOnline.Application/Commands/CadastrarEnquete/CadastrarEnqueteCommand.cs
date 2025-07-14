using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.CadastrarEnquete
{
    public record CadastrarEnqueteCommand(string Titulo, string Descricao, DateTime Encerramento, List<string> Opcoes) : ICommand<Guid>;
}
