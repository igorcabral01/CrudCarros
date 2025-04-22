// using CrudCarros.Models;
// using CrudCarros.Repositories;

// namespace CrudCarros.Services
// {
//     public class VendaService
//     {
//         private readonly VendaRepository _repository;

//         public VendaService(VendaRepository repository)
//         {
//             _repository = repository;
//         }

//         public IEnumerable<Venda> GetAll()
//         {
//             return _repository.GetAll();
//         }

//         public Venda GetById(int id)
//         {
//             return _repository.GetById(id);
//         }

//         public void Add(Venda venda)
//         {
//             _repository.Add(venda);
//         }

//         public void Update(Venda venda)
//         {
//             _repository.Update(venda);
//         }

//         public void Delete(int id)
//         {
//             _repository.Delete(id);
//         }
//     }
// }