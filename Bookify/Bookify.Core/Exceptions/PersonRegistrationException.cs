using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Core.Exceptions
{
    public class PersonRegistrationException : Exception
    {
        public PersonRegistrationException(string email)
        {
            this.Email = email;
        }

        public string Email { get; set; }
    }
}
