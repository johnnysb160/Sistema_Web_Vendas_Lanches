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
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria) || _lancheRepository.Lanche.Where(l =>
                                    l.Categoria.CategoriaNome.Equals(categoria)).Count() == 0)
            {
                lanches = _lancheRepository.Lanche.OrderBy(l => l.LancheId);
                categoria = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanche.Where(l =>
                                    l.Categoria.CategoriaNome.Equals(categoria)).OrderBy(l => l.Nome);

                categoriaAtual = categoria;
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

        public IActionResult Search(string searchString)
        {
            IEnumerable<Lanche> lanche;

            if (string.IsNullOrEmpty(searchString))
            {
                lanche = _lancheRepository.Lanche.OrderBy(x => x.LancheId);
            }
            else
            {
                lanche = _lancheRepository.Lanche.Where(x => x.Nome.ToLower().Contains(searchString.ToLower())).OrderBy(x => x.LancheId);
            }

            if (lanche.ToList().Count == 0)
            {
                return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanche = lanche, CategoriaAtual = $"Nenhum lanche encontrado com a palavra '{searchString}'"});
            }
            else
            {
                return View("~/Views/Lanche/List.cshtml", new LancheListViewModel { Lanche = lanche, CategoriaAtual = "Todos os Lanches" });
            }
        }
    }
}
