using Almocar2.Context;
using Almocar2.Models;
using Almocar2.Repositories.Interfaces;

namespace Almocar2.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }

}