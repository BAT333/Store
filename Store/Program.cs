using Store.Infrastructure;
using Store.Repositories;
using Store.Service;
/*
 * FAZER SERVICE
 * FAZER ROLLBACK
 * FAZER THROW
 * TERMINAR AS VALIDAÇÃO E VERIFICAR QUE TODA LOGICA FAZ SENTIDO 
 * SOLID / CLEAN 
 */

string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);

SqlConnectionProvider sqlConnection = new SqlConnectionProvider(value ?? "");
ClientService clientService2 = new ClientService(new ClientRepository(sqlConnection),new AddressesRepository(sqlConnection));

Console.ReadLine();