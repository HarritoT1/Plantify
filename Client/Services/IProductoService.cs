using Plantify.Shared;

namespace Plantify.Client.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista(string? gamaid);
    }
}
