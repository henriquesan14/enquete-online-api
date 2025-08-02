using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;
using EnqueteOnline.Application.Abstractions;

namespace EnqueteOnline.Application.Queries.BuscarEnquetePorId
{
    public record BuscarEnquetePorIdQuery(Guid Id) : IQuery<Result<EnqueteViewModel>>;
}
