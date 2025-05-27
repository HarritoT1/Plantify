using Plantify.Shared;

namespace Plantify.Client.Services
{
    public interface ILoginService
    {
        Task<ClienteDTO?> Log(CredencialDTO credenciales);

        Task<bool> IsAdmin(CredencialDTO credenciales);
    }
}
