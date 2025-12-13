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



try
{
    using HttpClient httpClient = new HttpClient();
    string respost = await httpClient.GetStringAsync("https://viacep.com.br/ws/01001000/json/");

    httpClient.Dispose();

    Console.WriteLine(respost);
}catch(Exception EX)
{
    EX.GetBaseException();
}


Console.ReadLine();