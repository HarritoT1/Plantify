using Plantify.Shared;
using System.Net.Http.Json;

namespace Plantify.Client.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _http;

        public PedidoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> Guardar(PedidoDTO pedidoDTO)
        {
            var response = await _http.PostAsJsonAsync("api/Pedido", pedidoDTO);
            var resultado = response.IsSuccessStatusCode;

            return resultado;
        }
    }
}
