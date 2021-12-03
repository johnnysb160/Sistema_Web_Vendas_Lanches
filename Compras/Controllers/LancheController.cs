using System;
using System.Collections.Generic;
using System.Linq;
using Compras.Models;
using Compras.Repositories.Interfaces;
using Compras.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _catergoriaRepository;

        public LancheController(ILancheRepository lancheRepository, ICategoriaRepository catergoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _catergoriaRepository = catergoriaRepository;
        }


        public IActionResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanche.OrderBy(l => l.LancheId);
                categoria = "Todos os Lanches";
            }
            else
            {
                if(string.Equals("Normal", _categoria, StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanche.Where(l =>
                    l.Categoria.CategoriaNome.Equals("Normal")).OrderBy(l => l.Nome);
                }
                else
                {
                    lanches = _lancheRepository.Lanche.Where(l =>
                    l.Categoria.CategoriaNome.Equals("Natural")).OrderBy(l => l.Nome);
                }

                categoriaAtual = _categoria;
            }

            var lancheListViewModel = new LancheListViewModel
            {
                Lanche = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanche.FirstOrDefault(x => x.LancheId == lancheId);

            if (lanche == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            else
            {
                return View(lanche);
            }
        }
    }
}
