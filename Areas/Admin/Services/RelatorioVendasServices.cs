using Almocar2.Context;
using Almocar2.Models;
using Microsoft.EntityFrameworkCore;

namespace Almocar2.Areas.Admin.Services
{
    public class RelatorioVendasServices
    {
        private readonly AppDbContext _context;
        public RelatorioVendasServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pedido>> BuscaPorData(DateTime? di,
        DateTime? df)
        {

            var queryPedidos = from obj in _context.Pedidos select obj; if (di.HasValue)
            {
                queryPedidos = queryPedidos.Where(p => p.PedidoEnviado >=

                di.Value);
            }
            if (df.HasValue)
            {
                queryPedidos = queryPedidos.Where(p => p.PedidoEnviado <=

                df.Value);
            }
            return await queryPedidos.Include(i =>
            i.PedidoItens).ThenInclude(m => m.Item).OrderBy(d =>
            d.PedidoEnviado).ToListAsync();
        }
    }
}