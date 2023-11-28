using Almocar2.Models;

namespace Almocar2.ViewModel
{
    public class PedidoItensViewModel
    {
        public Pedido Pedidos { get; set; }
        public IEnumerable<PedidoItem> PedidoItems { get; set; }
    }
}