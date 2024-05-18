using ClinicSoftware.Models;

namespace ClinicSoftware.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task AddClient(Client client);
        Task EditClient(Client client);
        Task DeleteClient(int clientId);
        Task<Client> GetClientById(int clientId);
        IEnumerable<Client> Clients { get; }
    }
}
