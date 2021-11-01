using System.Collections.Generic;
using Compras.Models;

namespace Compras.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LancheDestaque { get; set; }
    }
}
