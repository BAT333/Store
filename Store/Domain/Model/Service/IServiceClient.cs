
namespace Store.Domain.Model.Service
{
    internal interface IServiceClient<T> where T : class
    {
        Task<T> Add(T entity);
        bool Delete(int id);
        T? Update(int id, T entity);
        T? GetById(int id);
    }
}
