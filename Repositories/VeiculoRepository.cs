using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CrudCarros.Repositories
{
    public class VeiculoRepository
    {
        private readonly ApplicationDbContext _context;

        public VeiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veiculo>> ObterTodos()
        {
            return await _context.Veiculos
                .Include(v => v.Fabricante)
                .Where(v => v.Ativo)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterPorId(Guid id)
        {
            return await _context.Veiculos.FindAsync(id);
        }

        public async Task Adicionar(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var veiculo = await ObterPorId(id);
            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);
                await _context.SaveChangesAsync();
            }
        }
        
          public async Task<IEnumerable<Veiculo>> BuscarPorFabricanteEModeloAsync(string fabricante, string modelo)
          {
              return await Task.FromResult(_context.Veiculos
                  .Where(v => v.Fabricante!.Nome!.Contains(fabricante) && v.Nome!.Contains(modelo) && v.Ativo)
                  .ToList());
          }
        
        public async Task<IEnumerable<Veiculo>> BuscarPorFabricanteIdAsync(Guid fabricanteId)
        {
            return await _context.Veiculos
                .Where(v => v.FabricanteId.ToString() == fabricanteId.ToString() && v.Ativo)
                .ToListAsync();
        }
    }
}