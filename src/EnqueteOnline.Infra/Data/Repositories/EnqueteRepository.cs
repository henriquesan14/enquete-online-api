using EnqueteOnline.Application.Contracts.Data;
using EnqueteOnline.Domain.Entities;
using EnqueteOnline.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class EnqueteRepository : RepositoryBase<Enquete, EnqueteId>, IEnqueteRepository
    {
        public EnqueteRepository(EnqueteOnlineDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Enquete>> GetEnquetesComFiltroAsync(
            string titulo,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Enquete> query = DbContext.Enquetes;

            if (!string.IsNullOrEmpty(titulo))
            {
                query = query.Where(e => EF.Functions.ILike(e.Titulo, $"%{titulo}%"));
            }

            // Inclui as propriedades de navegação
            query = query
                .Include(e => e.Opcoes)
                .Include(e => e.Votos);

            return await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetEnquetesCountAsync(string titulo)
        {
            IQueryable<Enquete> query = DbContext.Enquetes;

            if (!string.IsNullOrEmpty(titulo))
            {
                query = query.Where(e => EF.Functions.ILike(e.Titulo, $"%{titulo}%"));
            }

            return await query.CountAsync();
        }
    }
}
