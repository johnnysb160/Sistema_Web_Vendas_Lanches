using System.Linq;
using Compras.Models;
using Compras.Repositories.Interfaces;
using Compras.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

            var itens = _carrinhoCompra.GetCarrinhoItem();
            _carrinhoCompra.CarrinhoCompraItem = itens;

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

            if (_carrinhoCompra.GetCarrinhoItem().Count > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("CarrinhoVazio");
            }

        }
    }
}
