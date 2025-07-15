using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;

namespace EnqueteOnline.Application.Commands.CadastrarEnquete
{
    public class CadastrarEnqueteCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CadastrarEnqueteCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarEnqueteCommand request, CancellationToken cancellationToken)
        {
            var enquete = Enquete.Create(request.Titulo, request.Descricao, request.Encerramento, request.Opcoes);

            await unitOfWork.Enquetes.AddAsync(enquete);
            await unitOfWork.CompleteAsync();

            return enquete.Id.Value;
        }
    }
}
