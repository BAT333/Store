using Store.Infrastructure;
using Store.Repositories;
using Store.Service;
using Store.Domain;
/*
 * FAZER THROW
 * TERMINAR AS VALIDAÇÃO E VERIFICAR QUE TODA LOGICA FAZ SENTIDO 
 * CRIAR ABSTRAÇÃO PARA QUE PRECISA
 * SOLID / CLEAN 
 */

string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);

SqlConnectionProvider sqlConnection = new SqlConnectionProvider(value ?? "");
StoreService storeService = new StoreService(new StoreRepository(sqlConnection),new ClientRepository(sqlConnection),new ProductRepository(sqlConnection) );

 Cart cart = storeService.Add(new Cart(27,5));
Console.WriteLine(cart.Id);

Cart cart1 = storeService.GetById(cart.Id);

Console.WriteLine(cart1.IdProduct + "---" + cart1.IdClient);

Cart cart2 = storeService.Update(cart.Id, new Cart(33, 2));

Console.WriteLine(cart2.IdProduct + "---" + cart2.IdClient);


bool delete =storeService.Delete(cart.Id);

Console.WriteLine(delete);

Console.ReadLine();