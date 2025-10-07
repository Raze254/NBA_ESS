using System.Web.Mvc;
using System.Web.Routing;

namespace Latest_Staff_Portal.CustomSecurity
{
    public class CustomAuthorization : FilterAttribute, IAuthorizationFilter
    {
        public string Role { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (Role.Contains(","))
                {
                    var array = Role.Split(',');
                    var j = false;
                    foreach (var c in array)
                    {
                        j = filterContext.HttpContext.User.IsInRole(c);
                        if (j) break;
                    }

                    if (!j)
                        filterContext.Result =
                            new RedirectToRouteResult(new RouteValueDictionary(new
                                { controller = "Common", action = "Unauthorized" }));
                }
                else
                {
                    if (!filterContext.HttpContext.User.IsInRole(Role))
                        filterContext.Result =
                            new RedirectToRouteResult(new RouteValueDictionary(new
                                { controller = "Common", action = "Unauthorized" }));
                }
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
        }
    }
}