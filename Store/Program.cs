using Store.Domain;
using Store.Infrastructure;
using Store.Repositories;
using Store.Service;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;
/*
 * CRIAR ABSTRAÇÃO PARA QUE PRECISA
 * APIS
 * UNIFICAR ENDEREÇO E CLIENTE 
 * SOLID / CLEAN 
 * Modelo ideal com Rule 
 */

string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);


SqlConnectionProvider sqlConnection = new SqlConnectionProvider(value ?? "");
StoreService storeService = new StoreService(new StoreRepository(sqlConnection),new ClientRepository(sqlConnection),new ProductRepository(sqlConnection));

Cart cart = storeService.Add(new Cart(20017, 10002));
Console.WriteLine(cart.Id);
Cart cart1 = storeService.Update(cart.Id, new Cart(20017, 10004));
Console.WriteLine(cart1.IdProduct);
Cart cart2 = storeService.GetById(cart.Id);
Console.WriteLine(cart2.IdProduct);
Console.WriteLine(storeService.Delete(cart.Id));

Console.ReadLine();