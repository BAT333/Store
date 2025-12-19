

using Store.Domain.Model.Dto;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Store.Domain
{
    internal class Address
    {
        private AddressDto da;

        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public int Number { get; set; }
        public string ZipCode { get; set; } = string.Empty;

        public Address(int id)
        {
            this.Id = id;
        }
        public Address(int id, string city, string state, string neighborhood, int number, string zipCode)
        {
            this.Id = id;
            this.City = city;
            this.State = state;
            this.Neighborhood = neighborhood;
            this.Number = number;
            this.ZipCode = zipCode;
        }
        public Address(string city, string state, string neighborhood, int number, string zipCode)
        {
            this.City = city;
            this.State = state;
            this.Neighborhood = neighborhood;
            this.Number = number;
            this.ZipCode = zipCode;
        }

        public Address(AddressDto dto,int num)
        {

            this.City = dto.City;
            this.State = dto.State;
            this.Neighborhood = dto.Neighborhood;
            this.Number = num;
            this.ZipCode = dto.ZipCode;

        }
    }
}
