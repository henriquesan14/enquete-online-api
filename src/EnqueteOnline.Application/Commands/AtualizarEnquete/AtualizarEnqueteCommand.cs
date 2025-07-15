using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.AtualizarEnquete
{
    public record AtualizarEnqueteCommand(Guid Id, string Titulo, string Descricao, DateTime Encerramento, List<string> Opcoes) : ICommand;
}
