using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
namespace Store.Domain.Model.Dto
{
    internal record AddressDto
    {
        [JsonPropertyName("localidade")]
        public string City { get; init; } = string.Empty;
        [JsonPropertyName("estado")]
        public string State { get; init; } = string.Empty;
        [JsonPropertyName("bairro")]
        public string Neighborhood { get; init; } = string.Empty;
        [JsonPropertyName("cep")]
        public string ZipCode { get; init; } = string.Empty;

        [JsonConstructor]
        public AddressDto(
      string city,
      string state,
      string neighborhood,
      string zipCode)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            Neighborhood = neighborhood ?? throw new ArgumentNullException( nameof(neighborhood));
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
        }
    }
}
