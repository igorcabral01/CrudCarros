using CrudCarros.Models;
using CrudCarros.Repositories;

namespace CrudCarros.Services
{
    public class ConcessionariaService
    {
        private readonly ConcessionariaRepository _repository;

        public ConcessionariaService(ConcessionariaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Concessionaria>> GetAll()
        {
            return await _repository.ObterTodos();
        }

        public async Task<Concessionaria?> GetById(Guid id)
        {
            return await _repository.ObterPorId(id);
        }

        public async Task Add(Concessionaria concessionaria)
        {
            await _repository.Adicionar(concessionaria);
        }

        public async Task Update(Concessionaria concessionaria)
        {
            await _repository.Atualizar(concessionaria);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Excluir(id);
        }
    }
}