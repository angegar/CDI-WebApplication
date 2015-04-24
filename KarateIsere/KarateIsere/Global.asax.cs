using KarateIsere.Controllers;
using KarateIsere.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KarateIsere {
    public class MvcApplication : System.Web.HttpApplication {
        
        protected void Application_Start () {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            BundleConfig.RegisterBundles( BundleTable.Bundles );
        }

        void Session_Start ( object sender, EventArgs e ) {
 
            if (User.Identity.IsAuthenticated) {
                ApplicationUserManager userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = userManager.FindByEmail( User.Identity.Name );
                Session.Add( "User", user );
            }
        }
    }
}
