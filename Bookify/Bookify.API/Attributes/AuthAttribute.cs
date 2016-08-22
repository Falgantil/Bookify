using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Bookify.Common.Exceptions;
using Bookify.DataAccess;
using Bookify.DataAccess.Repositories;
using Ninject;

namespace Bookify.API.Attributes
{
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute()
        {
            AuthenticationRepository = new AuthenticationRepository(new BookifyContext());
        }
        public Common.Repositories.IAuthenticationRepository AuthenticationRepository { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // get token from header
            var token = actionContext.Request.Headers.Authorization?.Parameter;
            if (token == null) throw new AuthenticationRequiredException("Missing authorization token");
            // validate token and return person
            var personAuthDto = AuthenticationRepository.VerifyToken(token).Result;
            var rolesList = Roles.Split(',');
            if (rolesList.Length == 0 || rolesList[0] == string.Empty)
                return true;
            foreach (var role in personAuthDto.AuthTokenDto.Roles)
            {
                if (rolesList.Any(x => x == role.Name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}