namespace Store.Domain.Model.Infrastructure
{
    internal interface IAddressSearcher<T> where T : class
    {
        Task<T> SearchByZipCod(string zipCod);
        Task<IReadOnlyList<T>> SearchByCity(string uf, string city,string neighborhood);

    }
}
