using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Bookify.API.Attributes
{
    public class RoleActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionExecutedContext)
        {
            
        }
    }
}