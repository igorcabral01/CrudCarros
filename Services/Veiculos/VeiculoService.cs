using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using CrudCarros.Repositories;

namespace CrudCarros.Services
{
    public class VeiculoService
    {
        private readonly VeiculoRepository _repository;

        public VeiculoService(VeiculoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Veiculo>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Veiculo?> ObterPorId(Guid id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Inserir(Veiculo veiculo)
        {
            await _repository.Adicionar(veiculo);
        }

        public async Task Atualizar(Veiculo veiculo)
        {
            await _repository.Atualizar(veiculo);
        }

        public async Task Excluir(Guid id)
        {
            await _repository.Excluir(id);
        }
    }
}