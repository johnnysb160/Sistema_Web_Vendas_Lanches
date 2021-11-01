using System.Linq;
using Compras.Models;
using Compras.Repositories.Interfaces;
using Compras.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            _carrinhoCompra.CarrinhoCompraItem = _carrinhoCompra.GetCarrinhoItem();

            var carrinhocompraViewModel = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };

            return View(carrinhocompraViewModel);
        }

        public RedirectToActionResult AdicionarItemCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanche.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemCarrinho(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanche.FirstOrDefault(x => x.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
