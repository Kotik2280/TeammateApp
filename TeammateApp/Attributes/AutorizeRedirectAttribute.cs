using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TeammateApp.Attributes
{
    public class AutorizeRedirectAttribute : ActionFilterAttribute
    {
        string _redirectRoute;
        public AutorizeRedirectAttribute(string redirectRoute = "Profile") 
        {
            _redirectRoute = redirectRoute;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(_redirectRoute, new RouteValueDictionary());
            }

            base.OnActionExecuting(context);
        }
    }
}
