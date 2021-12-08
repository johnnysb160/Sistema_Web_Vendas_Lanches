using Compras.Models;

namespace Compras.Repositories.Repositories
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
