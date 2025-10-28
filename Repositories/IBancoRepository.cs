using BoletosApi.Models;

namespace BoletosApi.Repositories
{
    public interface IBancoRepository
    {
        Task AddAsync(Banco banco);
        Task<IEnumerable<Banco>> GetAllAsync();
        Task<Banco?> GetByCodigoAsync(string codigo);
        Task<Banco?> GetByIdAsync(Guid id);
        Task UpdateAsync(Banco banco);
        Task DeleteAsync(Banco banco);
        Task SaveChangesAsync();
    }
}
