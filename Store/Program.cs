using Store.Infrastructure;
using Store.Repositories;
using Store.Models;
string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);

using Store.Models;
string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);
SqlConnectionProvider sqlConnection = new SqlConnectionProvider(value);

Console.ReadLine();