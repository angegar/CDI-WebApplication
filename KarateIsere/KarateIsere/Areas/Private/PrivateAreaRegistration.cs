using System.Web.Mvc;

namespace KarateIsere.Areas.Private
{
    public class PrivateAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Private";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Private_default",
                "Private/{controller}/{action}/{id}",
                new {
                    area=AreaName,
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
    }
}