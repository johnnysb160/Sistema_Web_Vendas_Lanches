﻿using Compras.Models;
using Microsoft.EntityFrameworkCore;

namespace Compras.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Lanche> Lanche { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}
