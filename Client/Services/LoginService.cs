using Azure;
using Plantify.Shared;
using System.Net.Http.Json;

namespace Plantify.Client.Services
{
    public class LoginService: ILoginService
    {
        private readonly HttpClient _http;
        public LoginService(HttpClient http)
        {
            this._http = http;
        }

        public async Task<ClienteDTO?> Log(CredencialDTO credenciales)
        {
            var response = await _http.PostAsJsonAsync("api/Login/validateUser", credenciales);

            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<ClienteDTO>();
                return cliente;
            }

            // Si no se encontró o hubo error, regresa null.
            return null;
        }

        public async Task<bool> IsAdmin(CredencialDTO credenciales)
        {
            var response = await _http.PostAsJsonAsync("api/Login/isadmin", credenciales);
            var resultado = response.IsSuccessStatusCode;

            return resultado;
        }
    }
}
