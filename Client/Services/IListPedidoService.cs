using Plantify.Shared;

namespace Plantify.Client.Services
{
    public interface IListPedidoService
    {
        Task<List<PedidoDTO>> Lista(string idCliente);

        Task<bool> UpdateVisibility(string namePedido);

        Task<PedidoDTO?> GetPedidoByName(string namePedido);
    }
}
