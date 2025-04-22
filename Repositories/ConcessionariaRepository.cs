using CrudCarros.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudCarros.Repositories
{
    public class ConcessionariaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConcessionariaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Concessionaria>> ObterTodos()
        {
            return await _context.Concessionarias.ToListAsync();
        }

        public async Task<Concessionaria?> ObterPorId(Guid id)
        {
            return await _context.Concessionarias.FindAsync(id);
        }

        public async Task Adicionar(Concessionaria concessionaria)
        {
            await _context.Concessionarias.AddAsync(concessionaria);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Concessionaria concessionaria)
        {
            _context.Concessionarias.Update(concessionaria);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var concessionaria = await ObterPorId(id);
            if (concessionaria != null)
            {
                _context.Concessionarias.Remove(concessionaria);
                await _context.SaveChangesAsync();
            }
        }
    }
}