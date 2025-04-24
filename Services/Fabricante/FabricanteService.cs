using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using CrudCarros.Repositories;

namespace CrudCarros.Services
{
    public class FabricanteService
    {
        private readonly FabricanteRepository _repository;

        public FabricanteService(FabricanteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Fabricante>> GetAll()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Fabricante?> GetById(Guid id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Add(Fabricante fabricante)
        {
            await _repository.Adicionar(fabricante);
        }

        public async Task Update(Fabricante fabricante)
        {
            await _repository.Atualizar(fabricante);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Excluir(id);
        }
    }
}