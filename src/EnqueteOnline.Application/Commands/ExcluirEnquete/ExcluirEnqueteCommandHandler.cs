using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Exceptions;
using EnqueteOnline.Domain.ValueObjects;
using MediatR;

namespace EnqueteOnline.Application.Commands.ExcluirEnquete
{
    public class ExcluirEnqueteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService) : ICommandHandler<ExcluirEnqueteCommand, Unit>
    {
        public async Task<Unit> Handle(ExcluirEnqueteCommand request, CancellationToken cancellationToken)
        {
            var enquete = await unitOfWork.Enquetes.GetByIdAsync(EnqueteId.Of(request.Id));
            if (enquete == null) throw new EnqueteNotFoundException(request.Id);
            if (enquete.CreatedBy != currentUserService.UserId) throw new ForbiddenAccessException();


            unitOfWork.Enquetes.Remove(enquete);

            await unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
