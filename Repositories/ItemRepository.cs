using Almocar2.Context;
using Almocar2.Models;
using Almocar2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Almocar2.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> Itens => _context.Itens.Include(c => c.Categoria);
        public IEnumerable<Item> ItensEmDestaque => _context.Itens.Where(i => i.Destaque).Include(c => c.Categoria);

        public Item GetItemById(int itemId)
        {
            return _context.Itens.FirstOrDefault(i => i.ItemId == itemId);
        }
    }
}