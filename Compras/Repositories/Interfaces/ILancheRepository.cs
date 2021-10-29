using System.Collections.Generic;
using Compras.Models;

namespace Compras.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanche { get; }
        IEnumerable<Lanche> LancheDestaque { get; }
        Lanche GetLancheById(int lancheId);
    }
}
