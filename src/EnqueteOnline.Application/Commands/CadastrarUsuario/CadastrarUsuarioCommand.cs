using EnqueteOnline.Application.Contracts.CQRS;

namespace EnqueteOnline.Application.Commands.CadastrarUsuario
{
    public record CadastrarUsuarioCommand(string Nome, string Email, string? AvatarUrl) : ICommand<Guid>;
}
