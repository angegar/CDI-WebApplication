using System.Web.Mvc;

namespace KarateIsere.Areas.Public {
    public class PublicAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Public";
            }
        }

        public override void RegisterArea ( AreaRegistrationContext context ) {
            context.MapRoute(
                "Public_default",
                "Public/{controller}/{action}/{id}",
                new {
                    area=AreaName,
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}