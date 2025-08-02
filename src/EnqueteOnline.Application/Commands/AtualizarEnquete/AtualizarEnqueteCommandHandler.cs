using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Linq.Expressions;
using System.Net;

namespace EnqueteOnline.Application.Commands.AtualizarEnquete
{
    public class AtualizarEnqueteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<AtualizarEnqueteCommand, Result>
    {
        public async Task<Result> Handle(AtualizarEnqueteCommand request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Enquete, object>>> includes = new List<Expression<Func<Enquete, object>>>
            {
                e => e.Votos,
                e => e.Opcoes
            };
            var enquete = await unitOfWork.Enquetes.GetByIdAsync(EnqueteId.Of(request.Id), includes: includes);
            if (enquete == null) return Result.Failure("Enquete não encontrada", HttpStatusCode.NotFound);
            if (enquete.CreatedBy != currentUserService.UserId) return Result.Failure("Acesso negado", HttpStatusCode.Forbidden);

            enquete.Update(request.Titulo, request.Descricao, request.Encerramento, request.Opcoes);

            await unitOfWork.CompleteAsync();

            return Result.Success(statusCode: HttpStatusCode.NoContent);
        }
    }
}
