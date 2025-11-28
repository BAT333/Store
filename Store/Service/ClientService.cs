using Store.Domain;
using Store.Infrastructure;
using Store.Model;
using Store.Repositories;
using System;
using System.Collections.Generic;
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
            if (entity.Address.Id > 1)
            {
                Client client = this._clientRepository.Add(entity);
                return client;
            }
            else
            {
                throw new InvalidOperationException("address not registered");
            }

        }

        public bool Delete(int id)
        {
            Client? client = GetById(id);
            if (client == null || client.Address == null) return false;
            if (!this._clientRepository.Delete(id)) return false;
            return this._addressesRepository.Delete(client.Address.Id);
        }

        public Client? GetById(int id)
        {
            Client? client = this._clientRepository.GetById(id);
            if (client.Address.Id <= 0) return null;
            client.Address = _addressesRepository.GetById(client.Address.Id);

            return client;
        }

        public Client? Update(int id, Client entity)
        {
            if(this._clientRepository.GetById(id) == null) return null;
            if (entity == null) return null;
            Client? client = this._clientRepository.Update(id, entity);
            return client;
        }
    }
}
