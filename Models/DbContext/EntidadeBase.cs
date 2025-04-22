

namespace CrudCarros.Models.DbContext
{
    public class EntidadeBase
    {
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now; 
    }
}