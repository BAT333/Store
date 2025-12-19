using Store.Domain;
using Store.Domain.Model.Dao;
using Store.Domain.Model.Dto;
using Store.Domain.Model.Infrastructure;
using Store.Domain.Model.Service;
using Store.Infrastructure.ExceptionCustomized;

namespace Store.Service
{
    internal class ClientService : IServiceClient<Client>
    {
        private readonly IDaoClient<Client> _clientRepository;
        private readonly IAddressSearcher<AddressDto> _addressesSearcher;
        public ClientService(IDaoClient<Client> clientRepository, IAddressSearcher<AddressDto> addressesSearcher)
        {
            this._clientRepository = clientRepository;
            _addressesSearcher = addressesSearcher;
        }

        public async Task<Client> Add(Client entity)
        {
            var dto = await _addressesSearcher.SearchByZipCod(entity.Address.ZipCode);
            //validar numero esta prenchido
            entity.Address = new Address(dto, entity.Address.Number);
            if (entity.Address.Id <= 0) throw new ExceptionalCustomer("address not registered");
            Client client = this._clientRepository.Add(entity);
            if (client.Id <= 0) throw new ExceptionalCustomer("Client not registered");
            return client;
        }

        public bool Delete(int id)
        {
            Client client = GetById(id);
            if (!this._clientRepository.Delete(id, client.Address.Id)) throw new ExceptionalCustomer("Failed to delete client.");
            return true;
        }

        public Client GetById(int id)
        {
            Client? client = this._clientRepository.GetById(id);
            if (client == null || client.Address.Id <= 0) throw new ExceptionalCustomer("Client not registered.");
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
