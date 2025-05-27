using Plantify.Shared;
using System.Net.Http.Json;

namespace Plantify.Client.Services
{
    public class ListPedidoService: IListPedidoService
    {
        private readonly HttpClient _http;
        public ListPedidoService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<List<PedidoDTO>> Lista(string idCliente)
        {
            var lista = new List<PedidoDTO>();

            lista = await _http.GetFromJsonAsync<List<PedidoDTO>>("api/ListPedido/pedidosUser/" + idCliente);

            return lista!;
        }

        public async Task<bool> UpdateVisibility(string namePedido) {
            var response = await _http.PutAsync($"api/ListPedido/updatePedido/{Uri.EscapeDataString(namePedido)}", null);
            var resultado = response.IsSuccessStatusCode;

            return resultado;
        }

        public async Task<PedidoDTO?> GetPedidoByName(string namePedido)
        {
            try
            {
                var pedidoDTO = await _http.GetFromJsonAsync<PedidoDTO>($"api/ListPedido/getPedido/{Uri.EscapeDataString(namePedido)}");
                return pedidoDTO;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error de HTTP: {ex.Message}");
                return null;
            }
        }
    }
}
