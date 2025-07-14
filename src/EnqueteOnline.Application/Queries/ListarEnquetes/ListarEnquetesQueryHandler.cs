using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.Pagination;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Domain.Entities;
using System.Linq.Expressions;

namespace EnqueteOnline.Application.Queries.ListarEnquetes
{
    public class ListarEnquetesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<ListarEnquetesQuery, PaginatedResult<EnqueteViewModel>>
    {
        public async Task<PaginatedResult<EnqueteViewModel>> Handle(ListarEnquetesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Enquete, bool>> predicate = e => string.IsNullOrEmpty(request.Titulo) || e.Titulo.ToLower().Contains(request.Titulo.ToLower());
            List<Expression<Func<Enquete, object>>> includes = new List<Expression<Func<Enquete, object>>>
            {
                e => e.Opcoes,
                e => e.Votos
            };
            Func<IQueryable<Enquete>, IOrderedQueryable<Enquete>> orderBy = m => m.OrderByDescending(mm => mm.CreatedAt!.Value);

            var enquetes = await unitOfWork.Enquetes.GetAsync(predicate, includes: includes, orderBy: orderBy, pageNumber: request.PageNumber, pageSize: request.PageSize);
            var countEnquetes = await unitOfWork.Enquetes.GetCountAsync(predicate);

            return new PaginatedResult<EnqueteViewModel>(request.PageNumber, request.PageSize, countEnquetes, enquetes.ToViewModelList());
        }
    }
}
