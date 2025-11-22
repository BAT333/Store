using Store.Infrastructure;
using Store.Repositories;
using Store.Models;
string? value = Environment.GetEnvironmentVariable(
    "DB_CONNECTION",
    EnvironmentVariableTarget.Machine
);

