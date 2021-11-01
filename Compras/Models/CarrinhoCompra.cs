using System;
using System.Collections.Generic;
using System.Linq;
using Compras.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Compras.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;
        public CarrinhoCompra(AppDbContext contexto)
        {
            _context = contexto;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItem { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Define uma sessão acessando o contexto atual(tem que registrar o IServiceCollection)
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtém um serviço do tipo do nosso contexto.
            var context = services.GetService<AppDbContext>();

            // Obtém ou gera o Id do carrinho.
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o Id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o carrinho com o contexto atual e o Id atribuido ou abtido.
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrinhoCompraItem.SingleOrDefault(
                    x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            // Verifica se o carrinho existe e senão existir cria um
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItem.Add(carrinhoCompraItem);
            }
            else  // Se existir o carrinho com o item, então incrementa a quantidade
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem =
                _context.CarrinhoCompraItem.SingleOrDefault(
                    x => x.Lanche.LancheId == lanche.LancheId && x.CarrinhoCompraId == CarrinhoCompraId);

            int quantidadeAux = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1) 
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeAux = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItem.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();

            return quantidadeAux;
        }

        public List<CarrinhoCompraItem> GetCarrinhoItem()
        {
            return CarrinhoCompraItem ??
                (CarrinhoCompraItem = _context.CarrinhoCompraItem.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                .Include(y => y.Lanche)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhotem = _context.CarrinhoCompraItem
                              .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItem.RemoveRange(carrinhotem);

            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            return _context.CarrinhoCompraItem.Where(x => x.CarrinhoCompraId == CarrinhoCompraId)
                .Select(x => x.Lanche.Preco * x.Quantidade).Sum();
        }
    }
}
