using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compras.Models;
using Compras.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Compras.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = new List<CarrinhoCompraItem>() { new CarrinhoCompraItem(), new CarrinhoCompraItem() };

            _carrinhoCompra.CarrinhoCompraItem = itens;


           //_carrinhoCompra.CarrinhoCompraItem = _carrinhoCompra.GetCarrinhoItem();

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraVM);
        }
    }
}
