using CrudCarros.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudCarros.Repositories
{
    public class VendaRepository
    {
        private readonly ApplicationDbContext _context;

        public VendaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            return await _context.Vendas.ToListAsync();
        }

        public async Task<Venda?> GetByIdAsync(Guid id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        public async Task AddAsync(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var venda = await GetByIdAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
        }
    }
}