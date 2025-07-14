using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;

namespace EnqueteOnline.Application.Commands.CadastrarEnquete
{
    public class CadastrarEnqueteCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CadastrarEnqueteCommand, Guid>
    {
        public async Task<Guid> Handle(CadastrarEnqueteCommand request, CancellationToken cancellationToken)
        {
            var opcoes = request.Opcoes.Select(o => OpcaoEnquete.Create(o)).ToList();
            var enquete = Enquete.Create(request.Titulo, request.Descricao, request.Encerramento, opcoes);

            await unitOfWork.Enquetes.AddAsync(enquete);
            await unitOfWork.CompleteAsync();

            return enquete.Id.Value;
        }
    }
}
