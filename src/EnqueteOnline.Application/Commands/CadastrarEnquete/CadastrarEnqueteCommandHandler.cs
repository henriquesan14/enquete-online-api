using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using System.Net;

namespace EnqueteOnline.Application.Commands.CadastrarEnquete
{
    public class CadastrarEnqueteCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CadastrarEnqueteCommand, Result<EnqueteViewModel>>
    {
        public async Task<Result<EnqueteViewModel>> Handle(CadastrarEnqueteCommand request, CancellationToken cancellationToken)
        {
            var enquete = Enquete.Create(request.Titulo, request.Descricao, request.Encerramento, request.Opcoes);

            await unitOfWork.Enquetes.AddAsync(enquete);
            await unitOfWork.CompleteAsync();

            return Result<EnqueteViewModel>.Success(enquete.ToViewModel(), statusCode: HttpStatusCode.Created);
        }
    }
}
