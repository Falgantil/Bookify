using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Ninject;

namespace Bookify.API.Attributes
{
    public class AuthAttribute : AuthorizeAttribute
    {

        [Inject]
        public Common.Repositories.IAuthenticationRepository AuthenticationRepository { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // get token from header
            var token = actionContext.Request.Headers.Authorization.Parameter;
            // validate token and return person
            var personAuthDto = AuthenticationRepository.VerifyToken(token).Result;
            var rolesList = Roles.Split(',');
            if (rolesList.Length == 0)
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