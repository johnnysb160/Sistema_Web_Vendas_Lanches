using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public IActionResult List()
        {
            ViewBag.Lanche = "Lanche";
            ViewData["Categoria"] = "Categoria";

            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanche = _lancheRepository.Lanche;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheListViewModel);
        }
    }
}
