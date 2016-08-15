using Bookify.Models;

namespace Bookify.Core.Attributes
{
    public class Authenticate
    {
        public static bool Authorization(string token, params Role[] roles)
        {
            // Check here for the Access token 
            //
            // -> var person = AccessTokenService.Validate(token) 
            // 
            // check if the PersonId can be found in the Database :D 
            //
            // -> roles[] = personRole.GetPersonRoles(person);
            //
            // return true if 1 of more of the role are equal 
            return true;
        }
    }
}
