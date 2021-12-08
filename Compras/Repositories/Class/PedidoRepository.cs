using System;
using System.Collections.Generic;
using System.Linq;
using Compras.Context;
using Compras.Models;
using Compras.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Compras.Repositories.Class
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

            foreach (var carrinhoItem in _carrinhoCompra.CarrinhoCompraItem)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    Quantidade = carrinhoItem.Quantidade,
                    LancheId = carrinhoItem.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Lanche.Preco
                };

                _context.PedidoDetalhes.Add(pedidoDetalhe);
            }
            _context.SaveChanges();
        }
    }
}
