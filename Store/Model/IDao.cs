namespace Store.Model
{
    internal interface IDao<T>
    {
        T Add(T entity);
        bool Delete(int id);
        T? Update (int id, T entity);
        T? GetById(int id);
       // Treturn GetAll<Treturn>();
    }
}
