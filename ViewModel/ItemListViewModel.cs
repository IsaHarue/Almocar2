using Almocar2.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Almocar2.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Itens { get; set; }
        public string CategoriaAtual {get; set;}
    }
}