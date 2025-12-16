

/*
 * arruma logica do endereço 
 * UNIFICAR ENDEREÇO E CLIENTE 
 */

using Store.Domain;
using Store.Infrastructure;
using Store.Repositories;
using Store.Service;

string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);
SqlConnectionProvider sqlConnectionProvider = new SqlConnectionProvider(value);


try
{
    using HttpClient httpClient = new HttpClient();
    AddressSearcher addressSearcher = new AddressSearcher(httpClient);
    ClientService clientService = new ClientService(new ClientRepository(sqlConnectionProvider), new AddressesRepository(sqlConnectionProvider), addressSearcher);

    
    await clientService.Add(new Client("rafaek", "rafq3el@gmail.com.br", "11934844243", new Address("","","",5, "01001-000")));

}catch(Exception EX)
{
    Console.WriteLine(EX.Message);
}


Console.ReadLine();