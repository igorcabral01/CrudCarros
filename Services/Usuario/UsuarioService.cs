using CrudCarros.Models;
using CrudCarros.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCarros.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _repository.Adicionar(usuario);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            await _repository.Atualizar(usuario);
        }

        public async Task ExcluirAsync(Guid id)
        {
            await _repository.Excluir(id);
        }
    }
}
