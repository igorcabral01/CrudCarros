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

        public async Task<IEnumerable<Fabricante>> ObterTodos()
        {
            return await _context.Fabricantes.ToListAsync();
        }

        public async Task<Fabricante?> ObterPorId(Guid id)
        {
            return await _context.Fabricantes.FindAsync(id);
        }

        public async Task Adicionar(Fabricante fabricante)
        {
            await _context.Fabricantes.AddAsync(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Fabricante fabricante)
        {
            _context.Fabricantes.Update(fabricante);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var fabricante = await ObterPorId(id);
            if (fabricante != null)
            {
                _context.Fabricantes.Remove(fabricante);
                await _context.SaveChangesAsync();
            }
        }
    }
}