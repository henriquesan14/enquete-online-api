using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Commands.CadastrarVoto
{
    public class CadastrarVotoCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IEnqueteNotificationService enqueteNotificationService) : ICommandHandler<CadastrarVotoCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarVotoCommand request, CancellationToken cancellationToken)
        {
            var voto = Voto.Create(EnqueteId.Of(request.EnqueteId), OpcaoEnqueteId.Of(request.OpcaoEnqueteId), currentUserService.IpAddress!);

            await unitOfWork.Votos.AddAsync(voto);
            await unitOfWork.CompleteAsync();

            await enqueteNotificationService.NotificarVotoAtualizadoAsync(request.EnqueteId);

            return voto.Id.Value;
        }
    }
}
