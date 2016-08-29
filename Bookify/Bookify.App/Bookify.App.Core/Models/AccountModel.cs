using Bookify.App.Core.Models;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class AccountModel : BaseModel
    {
        public AccountModel(AuthTokenDto token)
        {
            this.Token = token.Token;
            this.Person = token.Person;
            this.Roles = token.Roles;
        }

        public AccountModel()
        {

        }

        public PersonDto Person { get; set; }

        public string Token { get; set; }

        public string[] Roles { get; set; }
    }
}