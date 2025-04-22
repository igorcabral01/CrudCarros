using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CrudCarros.Repositories
{
    public class FabricanteRepository
    {
        private readonly ApplicationDbContext _context;

        public FabricanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fabricante>> GetAllAsync()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        public async Task<Fabricante?> GetByIdAsync(Guid id)
        {
            return await _context.Fabricantes.FindAsync(id);
        }

        public async Task AddAsync(Fabricante fabricante)
        {
            await _context.Fabricantes.AddAsync(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Fabricante fabricante)
        {
            _context.Fabricantes.Update(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var fabricante = await GetByIdAsync(id);
            if (fabricante != null)
            {
                _context.Fabricantes.Remove(fabricante);
                await _context.SaveChangesAsync();
            }
        }
    }
}