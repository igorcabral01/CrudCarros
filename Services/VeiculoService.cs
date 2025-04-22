// using CrudCarros.Models;
// using CrudCarros.Repositories;

// namespace CrudCarros.Services
// {
//     public class VeiculoService
//     {
//         private readonly VeiculoRepository _repository;

//         public VeiculoService(VeiculoRepository repository)
//         {
//             _repository = repository;
//         }

//         public IEnumerable<Veiculo> GetAll()
//         {
//             return _repository.GetAll();
//         }

//         public Veiculo GetById(int id)
//         {
//             return _repository.GetById(id);
//         }

//         public void Add(Veiculo veiculo)
//         {
//             _repository.Add(veiculo);
//         }

//         public void Update(Veiculo veiculo)
//         {
//             _repository.Update(veiculo);
//         }

//         public void Delete(int id)
//         {
//             _repository.Delete(id);
//         }
//     }
// }