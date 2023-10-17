using Almocar2.Models;
using Almocar2.Repositories.Interfaces;
using Almocar2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almocar2.Controllers
{

    public class ItemController : Controller
    {
        private readonly IItemRepository _ItemRespository;
        public ItemController(IItemRepository itemRespository)
        {
            _ItemRespository = itemRespository;
        }
        public IActionResult List(string categoria)
        {
            IEnumerable<Item> itens;
            var categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(categoria))
            {
                itens = _ItemRespository.Itens.OrderBy(m => m.ItemId);
                categoriaAtual = "Todos os itens";
            }
            else
            {
                itens = _ItemRespository.Itens.Where(m =>
                m.Categoria.Nome.Equals(categoria)).OrderBy(m => m.ItemId);

                categoriaAtual = categoria;
            }
            var itemListViewItem = new ItemListViewModel
            {
                Itens = itens,
                CategoriaAtual = categoriaAtual
            };
            return View(itemListViewItem);
        }

        public IActionResult Details(int itemId)
        {

            var item = _ItemRespository.Itens.FirstOrDefault(m =>

            m.ItemId == itemId);

            return View(item);
        }
        public IActionResult Search(string searchString)
        {

            IEnumerable<Item> itens;
            string categoriaAtual = string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                itens = _ItemRespository.Itens.OrderBy(m => m.Nome);
                categoriaAtual = "Todos os Itens";
            }
            else
            {
                itens = _ItemRespository.Itens.Where(m =>
                m.Nome.ToLower() == searchString.ToLower()).OrderBy(m => m.Nome);

                if (itens.Any())
                {
                    categoriaAtual = "Itens";
                }
                else
                {
                    categoriaAtual = "Nada encontrado";
                }
            }
            return View("~/Views/Item/List.cshtml", new
            ItemListViewModel
            {
                CategoriaAtual = categoriaAtual,
                Itens = itens
            });

        }
    }
}