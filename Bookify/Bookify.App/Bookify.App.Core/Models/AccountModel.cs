using Bookify.App.Core.Models;
using Bookify.Common.Models;

namespace Bookify.App.Core.Services
{
    public class AccountModel : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountModel"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public AccountModel(AuthTokenDto token)
        {
            this.Token = token.Token;
            this.Person = token.Person;
            this.Roles = token.Roles;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountModel"/> class.
        /// </summary>
        public AccountModel()
        {

        }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public PersonDto Person { get; set; }

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public string[] Roles { get; set; }
    }
}