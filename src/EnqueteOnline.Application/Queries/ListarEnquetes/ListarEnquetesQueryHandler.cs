using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Application.Extensions;
using EnqueteOnline.Application.Pagination;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Queries.ListarEnquetes
{
    public class ListarEnquetesQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<ListarEnquetesQuery, Result<PaginatedResult<EnqueteViewModel>>>
    {
        public async Task<Result<PaginatedResult<EnqueteViewModel>>> Handle(ListarEnquetesQuery request, CancellationToken cancellationToken)
        {
            var enquetes = await unitOfWork.Enquetes.GetEnquetesComFiltroAsync(request.Titulo!, request.PageNumber, request.PageSize);
            var countEnquetes = await unitOfWork.Enquetes.GetEnquetesCountAsync(request.Titulo!);

            var paginated = new PaginatedResult<EnqueteViewModel>(request.PageNumber, request.PageSize, countEnquetes, enquetes.ToViewModelList());

            return Result<PaginatedResult<EnqueteViewModel>>.Success(paginated);
        }
    }
}
