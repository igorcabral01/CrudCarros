// using CrudCarros.Models;
// using CrudCarros.Repositories;

// namespace CrudCarros.Services
// {
//     public class UsuarioService
//     {
//         private readonly UsuarioRepository _repository;

//         public UsuarioService(UsuarioRepository repository)
//         {
//             _repository = repository;
//         }

//         public IEnumerable<Usuario> GetAll()
//         {
//             return _repository.GetAll();
//         }

//         public Usuario GetById(int id)
//         {
//             return _repository.GetById(id);
//         }

//         public void Add(Usuario usuario)
//         {
//             _repository.Add(usuario);
//         }

//         public void Update(Usuario usuario)
//         {
//             _repository.Update(usuario);
//         }

//         public void Delete(int id)
//         {
//             _repository.Delete(id);
//         }
//     }
// }