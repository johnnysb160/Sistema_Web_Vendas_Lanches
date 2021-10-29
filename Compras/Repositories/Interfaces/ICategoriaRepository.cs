using System.Collections.Generic;
using Compras.Models;

namespace Compras.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categoria { get; }
    }
}
