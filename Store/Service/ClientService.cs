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
        private readonly IDaoAddresses<Address> _addressesRepository;
        private readonly IAddressSearcher<AddressDto> _addressesSearcher;
        public ClientService(IDaoClient<Client> clientRepository, IDaoAddresses<Address> addressesRepository, IAddressSearcher<AddressDto> addressesSearcher)
        {
            this._clientRepository = clientRepository;
            this._addressesRepository = addressesRepository;
            _addressesSearcher = addressesSearcher;
        }

        public async Task<Client> Add(Client entity)
        {
            var dto = await _addressesSearcher.SearchByZipCod(entity.Address.ZipCode);
            entity.Address = this._addressesRepository.Add(new Address(dto,entity.Address.Number));
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
