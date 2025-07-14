using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;

namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IEnqueteRepository : IAsyncRepository<Enquete, EnqueteId>
    {
    }
}
