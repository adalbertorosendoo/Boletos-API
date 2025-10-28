using BoletosApi.Data;
using BoletosApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace BoletosApi.Repositories
{
    public class BoletoRepository : IBoletoRepository
    {
        private readonly AppDbContext _context;
        public BoletoRepository(AppDbContext context) { _context = context; }

        public async Task AddAsync(Boleto boleto)
        {
            await _context.Boletos.AddAsync(boleto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Boleto>> GetAllAsync()
        {
            return await _context.Boletos.Include(b => b.Banco).ToListAsync();
        }

        public async Task<Boleto?> GetByIdAsync(Guid id)
        {
            return await _context.Boletos.Include(b => b.Banco).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Boleto boleto)
        {
            _context.Boletos.Update(boleto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Boleto boleto)
        {
            _context.Boletos.Remove(boleto);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
