using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Widgets.Testimonial
{
    public partial class RouteProvider : IRouteProvider
    {
        public int Priority
        {
            get
            {
                return 0;
            }
        }

        public void RegisterRoutes(RouteCollection routes)
        {


            // routes.MapRoute("Plugin.Testimonial.Edit",
            //     "Plugins/Testimonial/Edit",
            //     new { controller = "Testimonial", action = "Edit" },
            //     new[] { "Nop.Plugin.Widgets.Testimonial.Controllers" }
            //);

            routes.MapRoute("Plugin.Testimonial.Edit",
              "Plugins/Testimonial/Edit",
              new { controller = "Testimonial", action = "Edit" },
              new[] { "Nop.Plugin.Widgets.Testimonial.Controllers" }
         );

            routes.MapRoute("Plugin.Testimonial.Create",
               "Plugins/Testimonial/Create",
               new { controller = "Testimonial", action = "Create" },
               new[] { "Nop.Plugin.Widgets.Testimonial.Controllers" }
          );
        }
    }
}
