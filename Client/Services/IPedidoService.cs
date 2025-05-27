using Plantify.Shared;

namespace Plantify.Client.Services
{
    public interface IPedidoService
    {
        Task<bool> Guardar(PedidoDTO pedido);
    }
}
