using Store.Domain.Valid;

namespace Store.Domain
{
    internal class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private string _email;
        private string _phoneNumber;
        public Address Address { get; set; }

        public Client(int id, string name, string email, string phoneNumber, Address address)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
        }
        public Client(string name, string email, string phoneNumber, Address address)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
        }
        public Client(string name, string email, string phoneNumber)
        {
            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (IValidator.IsValidEmail(value))
                {
                    _email = value;
                }
                else
                {
                    throw new Exception("Invalid Email");
                }
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (IValidator.IsValidPhone(value.ToString()))
                {
                    _phoneNumber = value;
                }
                else
                {
                    throw new Exception("Invalid phone number");
                }
            }
        }

    }
}
