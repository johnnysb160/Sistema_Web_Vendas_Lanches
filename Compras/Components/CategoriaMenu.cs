using System.Linq;
using Compras.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categoria.OrderBy(x => x.CategoriaNome);

            return View(categorias);
        }
    }
}
