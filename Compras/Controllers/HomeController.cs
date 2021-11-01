using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Compras.Models;
using Compras.Repositories.Interfaces;
using Compras.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Compras.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewViewModel = new HomeViewModel
            {
                LancheDestaque = _lancheRepository.LancheDestaque
            };

            return View(homeViewViewModel);
        }
    }
}
