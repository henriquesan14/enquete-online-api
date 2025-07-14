namespace EnqueteOnline.Application.Contracts.Data
{
    public interface IUnitOfWork
    {
        IUsuarioRepository Usuarios { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IEnqueteRepository Enquetes { get; }
        IVotoRepository Votos { get; }
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}
