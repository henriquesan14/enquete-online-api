using EnqueteOnline.Application.Contracts.CQRS;
using EnqueteOnline.Application.ViewModels;

namespace EnqueteOnline.Application.Queries.BuscarEnquetePorId
{
    public record BuscarEnquetePorIdQuery(Guid Id) : IQuery<EnqueteViewModel>;
}
