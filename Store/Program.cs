using Store.Infrastructure;
using Store.Repositories;
using Store.Service;
using Store.Domain;
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
ProductService productService = new ProductService(new ProductRepository(sqlConnection));
Product product = productService.Add(new Product("Carro", "carro quebrado", 2.500));

Console.WriteLine(product.Id + " - " + product.Name);

Product product1 = productService.Update(product.Id, new Product("Carro Novo", "Carro Novo", 10.000));

Console.WriteLine(product1.Id + " - " + product1.Name);

Product product2 = productService.GetById(product.Id);

Console.WriteLine(product2.Id + " - " + product2.Name);

bool delete = productService.Delete(product.Id);

Console.WriteLine(delete);

Console.ReadLine();