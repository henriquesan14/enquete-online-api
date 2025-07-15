using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using MediatR;
using System.Linq.Expressions;

namespace EnqueteOnline.Application.Commands.AtualizarEnquete
{
    public class AtualizarEnqueteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<AtualizarEnqueteCommand, Unit>
    {
        public async Task<Unit> Handle(AtualizarEnqueteCommand request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Enquete, object>>> includes = new List<Expression<Func<Enquete, object>>>
            {
                e => e.Votos,
                e => e.Opcoes
            };
            var enquete = await unitOfWork.Enquetes.GetByIdAsync(EnqueteId.Of(request.Id), includes: includes);
            if (enquete == null) throw new EnqueteNotFoundException(request.Id);
            if (enquete.CreatedBy != currentUserService.UserId) throw new ForbiddenAccessException();

            enquete.Update(request.Titulo, request.Descricao, request.Encerramento, request.Opcoes);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
