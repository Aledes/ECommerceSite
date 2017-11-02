using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdrianBookStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //old tech

            //it's fine 99.99% fine with this route:
            //if someone goes to localhost:XXXX/, the "defaults" kick in,
            //routing the user to the index method of the Home controller.

            //if someone types in localhost: XXXX/Turkey, the "Turkey" portion
            //of the path is assumed to be the controller.
            //MVC will look for a TurkeyController in my project, and by default
            //look for an Index method on the turkey controller
            
            //for this reason, it's good pracftice to have an index method on

            //If someone types localhost:5555/Turkey/Gobble

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
