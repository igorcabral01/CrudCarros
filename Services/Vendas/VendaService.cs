using CrudCarros.Models;
using CrudCarros.Repositories;

namespace CrudCarros.Services
{
    public class VendaService
    {
        private readonly VendaRepository _repository;

        public VendaService(VendaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Venda>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Venda?> ObterPorId(Guid id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Inserir(Venda venda)
        {
            await _repository.Adicionar(venda);
        }

        public async Task Atualizar(Venda venda)
        {
            await _repository.Atualizar(venda);
        }

        public async Task Excluir(Guid id)
        {
            await _repository.Excluir(id);
        }
    }
}