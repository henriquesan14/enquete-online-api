using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using System.Linq.Expressions;
using System.Net;

namespace EnqueteOnline.Application.Queries.BuscarEnquetePorId
{
    public class BuscarEnquetePorIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<BuscarEnquetePorIdQuery, Result<EnqueteViewModel>>
    {
        public async Task<Result<EnqueteViewModel>> Handle(BuscarEnquetePorIdQuery request, CancellationToken cancellationToken)
        {
            List<Expression<Func<Enquete, object>>> includes = new List<Expression<Func<Enquete, object>>>
            {
                e => e.Opcoes,
                e => e.Votos
            };
            var enquete = await unitOfWork.Enquetes.GetByIdAsync(EnqueteId.Of(request.Id), includes: includes);
            if (enquete is null) return Result<EnqueteViewModel>.Failure("Enquete não encontrada", HttpStatusCode.NotFound);

            return Result<EnqueteViewModel>.Success(enquete.ToViewModel());
        }
    }
}
