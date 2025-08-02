using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.ValueObjects;
using System.Net;

namespace EnqueteOnline.Application.Commands.ExcluirEnquete
{
    public class ExcluirEnqueteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<ExcluirEnqueteCommand, Result>
    {
        public async Task<Result> Handle(ExcluirEnqueteCommand request, CancellationToken cancellationToken)
        {
            var enquete = await unitOfWork.Enquetes.GetByIdAsync(EnqueteId.Of(request.Id));
            if (enquete == null) return Result.Failure("Enquete não encontrada", HttpStatusCode.NotFound);
            if (enquete.CreatedBy != currentUserService.UserId) return Result.Failure("Acesso negado", HttpStatusCode.Forbidden);


            unitOfWork.Enquetes.Remove(enquete);

            await unitOfWork.CompleteAsync();

            return Result.Success(statusCode: HttpStatusCode.NoContent);
        }
    }
}
