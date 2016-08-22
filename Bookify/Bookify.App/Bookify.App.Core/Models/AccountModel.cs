using Bookify.App.Core.Models;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class AccountModel : BaseModel
    {
        public AccountModel(AuthTokenDto token, PersonDto person)
        {
            this.Token = token;
            this.Person = person;
        }

        public AccountModel()
        {

        }

        public PersonDto Person { get; set; }

        public AuthTokenDto Token { get; set; }

        public string[] Roles { get; set; }
    }
}