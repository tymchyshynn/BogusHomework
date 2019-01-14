using Bogus;
using System;


namespace hpmework9
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Password { get; set; }

        public User FillIn()
        {
             new Faker<User>()
                .StrictMode(true)
                .RuleFor(o => o.Name, f => f.Name.FirstName())
                .RuleFor(o => o.Surname, f => f.Name.LastName())
                .RuleFor(o => o.Email, (f, a) => f.Internet.Email(a.Name, a.Surname))
                .RuleFor(o => o.Company, f => f.Company.CompanyName())
                .RuleFor(o => o.Password, f => f.Internet.Password(12, false, "[\\W\a-zA-Z\\d]", "1@" ))
                .Populate(this);
            
            return this;
        }

    }

}
