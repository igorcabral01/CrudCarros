using CrudCarros.Models;
using CrudCarros.Models.DbContext;
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

        public async Task<IEnumerable<Concessionaria>> BuscarPorNomeOuLocalizacaoAsync(string termo)
        {
            return await _context.Concessionarias
                .Where(c => c.Nome!.Contains(termo) || c.CEP!.Contains(termo))
                .ToListAsync();
        }

         public async Task<IEnumerable<Fabricante>> BuscarPorNomeAsync(string termo)
        {
            return await _context.Fabricantes
                .Where(f => f.Nome!.Contains(termo))
                .ToListAsync();
        }
    }
}