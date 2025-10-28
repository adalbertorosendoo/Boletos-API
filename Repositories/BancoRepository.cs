using BoletosApi.Data;
using BoletosApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BoletosApi.Repositories
{
    public class BancoRepository : IBancoRepository
    {
        private readonly AppDbContext _context;
        public BancoRepository(AppDbContext context) { _context = context; }

        public async Task AddAsync(Banco banco)
        {
            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Banco>> GetAllAsync()
        {
            return await _context.Bancos.ToListAsync();
        }

        public async Task<Banco?> GetByCodigoAsync(string codigo)
        {
            return await _context.Bancos.FirstOrDefaultAsync(b => b.Codigo == codigo);
        }

        public async Task<Banco?> GetByIdAsync(Guid id)
        {
            return await _context.Bancos.FindAsync(id);
        }

        public async Task UpdateAsync(Banco banco)
        {
            _context.Bancos.Update(banco);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Banco banco)
        {
            _context.Bancos.Remove(banco);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
