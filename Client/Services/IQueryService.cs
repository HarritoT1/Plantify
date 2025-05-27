using Plantify.Shared;
using System.Data;

namespace Plantify.Client.Services
{
    public interface IQueryService
    {
        Task<List<Dictionary<string, object>>> GetQuery(int? idQuery);
    }
}
