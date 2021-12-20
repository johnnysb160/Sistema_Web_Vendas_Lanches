using System;
using Compras.Models;
using Compras.Repositories.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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


                ViewBag.CheckoutCompletoMensagem = "Obrigado e aproveite seu pedido!";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal().ToString("C2");

                _carrinhoCompra.LimparCarrinho();
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }

            return View(pedido);
        }
    }
}
