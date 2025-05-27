using System.Data;
using Azure;
using Plantify.Shared;
using System.Net.Http.Json;
using Microsoft.Data.SqlClient;

namespace Plantify.Client.Services
{
    public class QueryService : IQueryService
    {
        private readonly HttpClient _http;

        public QueryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Dictionary<string, object>>> GetQuery(int? idQuery)
        {
            try
            {
                if (idQuery != null)
                {
                    var url = $"api/Query/{idQuery}";
                    var result = await _http.GetFromJsonAsync<List<Dictionary<string, object>>>(url);
                    return result ?? new List<Dictionary<string, object>>();
                }
                else
                {
                    return new List<Dictionary<string, object>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetQuery: {ex.Message}");
                return new List<Dictionary<string, object>>();
            }
        }

    }
}
