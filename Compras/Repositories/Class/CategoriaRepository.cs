using System;
using System.Collections.Generic;
using Compras.Context;
using Compras.Models;
using Compras.Repositories.Interfaces;

namespace Compras.Repositories.Class
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categoria => _context.Categoria;
    }
}
