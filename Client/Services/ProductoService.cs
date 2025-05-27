using Plantify.Shared;
using System.Net.Http.Json;

namespace Plantify.Client.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;
        public ProductoService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<List<ProductoDTO>> Lista(string? gamaid)
        {
            var lista = new List<ProductoDTO>();

            lista = await _http.GetFromJsonAsync<List<ProductoDTO>>("api/Producto/gama/" + gamaid);

            return lista!;
        }
    }
}
