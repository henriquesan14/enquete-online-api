using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Contracts.Services;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Net;

namespace EnqueteOnline.Application.Commands.CadastrarVoto
{
    public class CadastrarVotoCommandHandler(IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService, IEnqueteNotificationService enqueteNotificationService) : ICommandHandler<CadastrarVotoCommand, Result<VotoViewModel>>
    {
        public async Task<Result<VotoViewModel>> Handle(CadastrarVotoCommand request, CancellationToken cancellationToken)
        {
            var voto = Voto.Create(EnqueteId.Of(request.EnqueteId), OpcaoEnqueteId.Of(request.OpcaoEnqueteId), currentUserService.IpAddress!);

            await unitOfWork.Votos.AddAsync(voto);
            await unitOfWork.CompleteAsync();

            await enqueteNotificationService.NotificarVotoAtualizadoAsync(request.EnqueteId);

            return Result<VotoViewModel>.Success(voto.ToViewModel(), statusCode: HttpStatusCode.Created);
        }
    }
}
