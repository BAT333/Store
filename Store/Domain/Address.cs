
namespace Store.Domain
{
    internal class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }

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

    }
}
