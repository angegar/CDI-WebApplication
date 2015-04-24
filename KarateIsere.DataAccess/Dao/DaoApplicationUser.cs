using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Security.Principal;
using System.Threading.Tasks;
using KarateIsere.DataAccess;

namespace KarateIsere.Models {
    public partial class ApplicationUser {
        /*
        public static async Task<ApplicationUser> Get ( IPrincipal user, IOwinContext ctx ) {
            ApplicationUserManager manager = ctx.GetUserManager<ApplicationUserManager>();
            ApplicationUser appUser = await manager.FindByEmailAsync( user.Identity.Name );
            return appUser;
        }
         * */
    }
}
