using ClinicSoftware.Context;
using ClinicSoftware.Models;
using ClinicSoftware.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicSoftware.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> Clients => _context.Clients;


        public async Task AddClient(Client client)
        {

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Objeto cliente não pode ser nulo.");
            }

            var existingClient = _context.Clients
                                            .FirstOrDefaultAsync(c => c.ClientPhoneNumber == client.ClientPhoneNumber);

            if (existingClient != null)
            {
                throw new Exception("Já existe um cliente com o mesmo número de telefone.");
            }

            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao tentar adicionar o cliente.", ex);
            }
        }

        public async Task EditClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "Objeto cliente não pode ser nulo.");
            }

            var existingClient = await _context.Clients.FindAsync(client.ClientId) ?? throw new InvalidOperationException("Não foi localizado o cliente.");

            _context.Entry(existingClient).CurrentValues.SetValues(client);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteClient(int clientId)
        {
            var existingClient = await _context.Clients.FindAsync(clientId) ?? throw new InvalidOperationException("Não foi localizado o cliente.");

            _context.Clients.Remove(existingClient);

            await _context.SaveChangesAsync();
        }
    }
}
