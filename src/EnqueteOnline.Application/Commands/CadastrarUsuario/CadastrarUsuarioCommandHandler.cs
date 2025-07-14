using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Linq.Expressions;

namespace EnqueteOnline.Application.Commands.CadastrarUsuario
{
    public class CadastrarUsuarioCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CadastrarUsuarioCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            Expression<Func<Usuario, bool>> predicate = u => u.Email == Email.Of(request.Email);
            var username = await unitOfWork.Usuarios.GetSingleAsync(predicate);
            if (username != null) throw new UserAlreadyExistsException(request.Email);

            var user = Usuario.Create(
                nome: request.Nome,
                email: Email.Of(request.Email),
                avatarUrl: request.AvatarUrl!
            );

            await unitOfWork.Usuarios.AddAsync(user);
            await unitOfWork.CompleteAsync();

            return user.Id.Value;
        }
    }
}
