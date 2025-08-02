using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.Pagination;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Queries.ListarEnquetes
{
    public record ListarEnquetesQuery(int PageNumber, int PageSize, string? Titulo) : IQuery<Result<PaginatedResult<EnqueteViewModel>>>;
}
