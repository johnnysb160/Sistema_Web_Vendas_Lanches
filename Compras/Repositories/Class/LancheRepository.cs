using System;
using System.Collections.Generic;
using System.Linq;
using Compras.Context;
using Compras.Models;
using Compras.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Compras.Repositories.Class
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _lanche;
        public LancheRepository(AppDbContext lanche)
        {
            _lanche = lanche;
        }

        public IEnumerable<Lanche> Lanche => _lanche.Lanche.Include(x => x.Categoria);

        public IEnumerable<Lanche> LancheDestaque => _lanche.Lanche.Where(y => y.IsLancheDestaque).Include(z => z.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            return _lanche.Lanche.FirstOrDefault(j => j.LancheId == lancheId);
        }
    }
}
