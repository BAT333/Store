using Store.Domain;
using Store.Infrastructure;
using Store.Repositories;
using Store.Service;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;
/*
 * FAZER THROW
 * TERMINAR AS VALIDAÇÃO E VERIFICAR QUE TODA LOGICA FAZ SENTIDO 
 * CRIAR ABSTRAÇÃO PARA QUE PRECISA
 * UNIFICAR ENDEREÇO E CLIENTE 
 * SOLID / CLEAN 
 * Modelo ideal com Rule 
 */

string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);


SqlConnectionProvider sqlConnection = new SqlConnectionProvider(value ?? "");

ClientService client = new ClientService(new ClientRepository(sqlConnection),new AddressesRepository(sqlConnection));

var cliet = client.Add(new Client("rafsadh","aydgah45dg@gmail.com","564564",new Address("spadlask", "dasdkaos", "asdasasdas",24,"5485544")));
Console.WriteLine(cliet.Id);
Console.ReadLine();