using System.Collections.Generic;
using Compras.Models;

namespace Compras.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanche { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
