using Almocar2.Models;

namespace Almocar2.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorias {get;}
    }
}