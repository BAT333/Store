using Store.Domain.Model.Dto;
using Store.Domain.Model.Infrastructure;
using Store.Infrastructure.ExceptionCustomized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Store.Infrastructure
{
    internal class AddressSearcher : IAddressSearcher<AddressDto>
    {
        private readonly HttpClient _httpClient;

        public AddressSearcher(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task<IReadOnlyList<AddressDto>> SearchByCity(string uf,string city, string neighborhood)
        {
            var url = $"https://viacep.com.br/ws/{Uri.EscapeDataString(uf)}/{Uri.EscapeDataString(city)}/{Uri.EscapeDataString(neighborhood)}/json/";

            var json = await _httpClient.GetStringAsync(url);

            return JsonSerializer.Deserialize<List<AddressDto>>(json) ?? throw new ExceptionalCustomer("Invalid API response");

        }

        public async Task<AddressDto> SearchByZipCod(string zipCod)
        {
            var url = $"https://viacep.com.br/ws/{Uri.EscapeDataString(zipCod)}/json/";

            var json = await _httpClient.GetStringAsync(url);

            return JsonSerializer.Deserialize<AddressDto>(json) ?? throw new ExceptionalCustomer("Invalid API response");
        }
    }
}
