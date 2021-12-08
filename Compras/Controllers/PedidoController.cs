using Compras.Models;
using Compras.Repositories.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            _carrinhoCompra.CarrinhoCompraItem = _carrinhoCompra.GetCarrinhoItem();

            if (_carrinhoCompra.CarrinhoCompraItem.Count == 0)
            {
                ModelState.AddModelError("", "Seu carrinho está vazio...");
            }

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);
                _carrinhoCompra.LimparCarrinho();
                return RedirectToAction("CheckoutCompleto");
            }

            return View(pedido);
        }

        public IActionResult CheckoutCompleto()
        {
            ViewBag.CheckoutCompletoMensagem = "Obrigado e aproveite seu pedido!";
            return View();
        }
    }
}
