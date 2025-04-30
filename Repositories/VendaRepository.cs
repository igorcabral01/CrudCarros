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

        public async Task<IEnumerable<Venda>> ObterTodos()
        {
            return await _context.Vendas.ToListAsync();
        }

        public async Task<Venda?> ObterPorId(Guid id)
        {
            return await _context.Vendas.FindAsync(id);
        }

        public async Task Adicionar(Venda venda)
        {
            await _context.Vendas.AddAsync(venda);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Venda venda)
        {
            _context.Vendas.Update(venda);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var venda = await ObterPorId(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Venda>> ObterPorConcessionariaId(Guid concessionariaId)
        {
            return await _context.Vendas
                .Where(v => v.ConcessionariaId == concessionariaId)
                .Include(v => v.veiculo)
                .Include(v => v.Fabricante)
                .ToListAsync();
        }
    }
}