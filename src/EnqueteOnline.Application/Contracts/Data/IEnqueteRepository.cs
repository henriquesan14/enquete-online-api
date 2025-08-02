using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IEnqueteRepository : IAsyncRepository<Enquete, EnqueteId>
    {
        Task<IReadOnlyList<Enquete>> GetEnquetesComFiltroAsync(string titulo, int pageNumber, int pageSize);
        Task<int> GetEnquetesCountAsync(string titulo);
    }
}
