using Store.Domain;
using Store.Infrastructure;
using Store.Infrastructure.ExceptionCustomized;
using Store.Model;
using Store.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service
{
    internal class ClientService : IDao<Client>
    {
        private readonly IDao<Client> _clientRepository;
        private readonly IDao<Address> _addressesRepository;
        public ClientService(IDao<Client> clientRepository, IDao<Address> addressesRepository)
        {
            this._clientRepository = clientRepository;
            this._addressesRepository = addressesRepository;
        }

        public Client Add(Client entity)
        {

            entity.Address = this._addressesRepository.Add(entity.Address);
            if (entity.Address.Id <= 0) throw new ExceptionalCustomer("address not registered");
            Client client = this._clientRepository.Add(entity);
            if (client.Id <= 0) throw new ExceptionalCustomer("Client not registered");
            return client;

        }

        public bool Delete(int id)
        {
            Client client = GetById(id);
            if (!this._clientRepository.Delete(id)) throw new ExceptionalCustomer("Failed to delete client.");
            if (!this._addressesRepository.Delete(client.Address.Id)) throw new ExceptionalCustomer("Failed to delete Address.");
            return true;
        }

        public Client GetById(int id)
        {
            Client? client = this._clientRepository.GetById(id);
            if (client == null || client.Address.Id <= 0) throw new ExceptionalCustomer("Client not registered.");

            Address? address = _addressesRepository.GetById(client.Address.Id);
            if (address == null || address.Id <= 0) throw new ExceptionalCustomer("address not registered.");

            client.Address = address;

            return client;
        }

        public Client? Update(int id, Client entity)
        {
            if (entity == null) throw new ExceptionalCustomer("Client not registered.");
            this._clientRepository.GetById(id);
            Client? client = this._clientRepository.Update(id, entity);
            return client;
        }
    }
}
