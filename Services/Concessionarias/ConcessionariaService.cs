using CrudCarros.Models;
using CrudCarros.Repositories;
using CrudCarros.Services.Concessionarias;

namespace CrudCarros.Services
{
    public class ConcessionariaService
    {
        private readonly ConcessionariaRepository _repository;

        public ConcessionariaService(ConcessionariaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConcessionariaDto>> ObterTodos()
        {
            var concessionarias = await _repository.ObterTodos();
            return concessionarias.Select(c => new ConcessionariaDto
            {
                ConcessionariaId = c.ConcessionariaId,
                Nome = c.Nome,
                Rua = c.Rua,
                Cidade = c.Cidade,
                Estado = c.Estado,
                CEP = c.CEP,
                Telefone = c.Telefone,
                Email = c.Email,
                CapacidadeMaximaVeiculos = c.CapacidadeMaximaVeiculos
            });
        }

        public async Task<ConcessionariaDto?> ObterPorId(Guid id)
        {
            var concessionaria = await _repository.ObterPorId(id);
            if (concessionaria == null) return null;

            return new ConcessionariaDto
            {
            Nome = concessionaria.Nome,
            Rua = concessionaria.Rua,
            Cidade = concessionaria.Cidade,
            Estado = concessionaria.Estado,
            CEP = concessionaria.CEP,
            Telefone = concessionaria.Telefone,
            Email = concessionaria.Email,
            CapacidadeMaximaVeiculos = concessionaria.CapacidadeMaximaVeiculos
            };
        }

        public async Task<Concessionaria> Inserir(ConcessionariaDto concessionariaDto)
        {
            var concessionaria = new Concessionaria
            {
                Nome = concessionariaDto.Nome,
                Rua = concessionariaDto.Rua,
                Cidade = concessionariaDto.Cidade,
                Estado = concessionariaDto.Estado,
                CEP = concessionariaDto.CEP,
                Telefone = concessionariaDto.Telefone,
                Email = concessionariaDto.Email,
                CapacidadeMaximaVeiculos = concessionariaDto.CapacidadeMaximaVeiculos
            };
            await _repository.Adicionar(concessionaria);
            return concessionaria;
        }

        public async Task Atualizar(Concessionaria concessionaria)
        {
            await _repository.Atualizar(concessionaria);
        }

        public async Task Excluir(Guid id)
        {
            await _repository.Excluir(id);
        }

        public async Task<Concessionaria?> ObterEntidadePorId(Guid id)
        {
            return await _repository.ObterPorId(id);
        }
    }
}