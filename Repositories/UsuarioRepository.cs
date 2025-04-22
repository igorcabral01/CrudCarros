using CrudCarros.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudCarros.Repositories
{
    public class UsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> ObterTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Guid id)
        {
            var usuario = await ObterPorId(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}