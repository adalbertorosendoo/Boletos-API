using BoletosApi.Models;

namespace BoletosApi.Repositories
{
    public interface IBoletoRepository
    {
        Task AddAsync(Boleto boleto);
        Task<IEnumerable<Boleto>> GetAllAsync();
        Task<Boleto?> GetByIdAsync(Guid id);
        Task UpdateAsync(Boleto boleto);
        Task DeleteAsync(Boleto boleto);
        Task SaveChangesAsync();
    }
}
