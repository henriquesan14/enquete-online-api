using EnqueteOnline.Application.Contracts.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnqueteOnline.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;
        private readonly EnqueteOnlineDbContext _dbContext;

        public UnitOfWork(EnqueteOnlineDbContext dbContext, IUsuarioRepository usuarios, IRefreshTokenRepository refreshTokens, IEnqueteRepository enquetes, IVotoRepository votos)
        {
            _dbContext = dbContext;
            Usuarios = usuarios;
            RefreshTokens = refreshTokens;
            Enquetes = enquetes;
            Votos = votos;
        }

        public IUsuarioRepository Usuarios { get; }

        public IRefreshTokenRepository RefreshTokens { get; }

        public IEnqueteRepository Enquetes { get; }
        public IVotoRepository Votos { get; }

        public async Task BeginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            IsDisposing(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void IsDisposing(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
