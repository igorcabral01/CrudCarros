using CrudCarros.Models;
using CrudCarros.Models.DbContext;
using CrudCarros.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudCarros.Services.Vendas
{
    public class VendaService
    {
        private readonly VendaRepository _vendaRepository;
        private readonly ConcessionariaRepository _concessionariaRepository;
        private readonly FabricanteRepository _fabricanteRepository;
        private readonly VeiculoRepository _veiculoRepository;

        public VendaService(VendaRepository vendaRepository, ConcessionariaRepository concessionariaRepository, FabricanteRepository fabricanteRepository, VeiculoRepository veiculoRepository)
        {
            _vendaRepository = vendaRepository;
            _concessionariaRepository = concessionariaRepository;
            _fabricanteRepository = fabricanteRepository;
            _veiculoRepository = veiculoRepository;
        }

        public async Task<string> RealizarVendaAsync(Venda venda)
        {
            if (venda.DataVenda > DateTime.Now)
                throw new Exception("A data da venda não pode ser futura.");

            var veiculo = await _veiculoRepository.ObterPorId(Guid.Parse(venda.VeiculoId.ToString()));
            if (veiculo == null || venda.PrecoVenda > veiculo.Preco)
                throw new Exception("O preço de venda deve ser menor ou igual ao preço do veículo.");

            veiculo.Ativo = false;
            await _veiculoRepository.Atualizar(veiculo);

            venda.NumeroProtocolo = Guid.NewGuid().ToString();
            await _vendaRepository.Adicionar(venda);

            return venda.NumeroProtocolo;
        }

        public async Task<IEnumerable<Concessionaria>> BuscarConcessionariasAsync(string termo)
        {
            return await _concessionariaRepository.BuscarPorNomeOuLocalizacaoAsync(termo);
        }

        // public async Task<IEnumerable<Fabricante>> BuscarFabricantesAsync(string nome)
        // {
        //     return await _fabricanteRepository.ObterPorNome(nome);
        // }

        public async Task<IEnumerable<Veiculo>> BuscarVeiculosAsync(string fabricante, string modelo)
        {
            return await _veiculoRepository.BuscarPorFabricanteEModeloAsync(fabricante, modelo);
        }

        public async Task<IEnumerable<Veiculo>> BuscarVeiculosPorFabricanteAsync(Guid fabricanteId)
        {
            return await _veiculoRepository.BuscarPorFabricanteIdAsync(fabricanteId);
        }

        public async Task<IEnumerable<Venda>> ObterVendasPorConcessionariaId(Guid concessionariaId)
        {
            return await _vendaRepository.ObterPorConcessionariaId(concessionariaId);
        }
    }
}