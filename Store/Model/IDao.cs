namespace Store.Model
{
    internal interface IDao<T>
    {
        T Add(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        T? Update(int id, T entity);
        T? GetById(int id);
        // Treturn GetAll<Treturn>();
    }
}
