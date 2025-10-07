using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace Latest_Staff_Portal.CustomSecurity
{
    public class CustomeAuthentication : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
        }
    }
}