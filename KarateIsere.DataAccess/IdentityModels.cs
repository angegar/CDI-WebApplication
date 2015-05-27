using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using KarateIsere.DataAccess;
using KarateContext = KarateIsere.DataAccess;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarateIsere.Models {
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public partial class ApplicationUser : IdentityUser {

        public ApplicationUser () {

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync ( UserManager<ApplicationUser> manager ) {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync( this, DefaultAuthenticationTypes.ApplicationCookie );
            
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }

        public virtual Club Club {
            get;
            set;
        }

        //[ForeignKey( "Club" )]
        public virtual string NumAffiliation {
            get;
            set;
        }
    }

    //public class ApplicationDbContext : KarateIsere<ApplicationUser> {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext ()
            : base( "KarateIsere", throwIfV1Schema: false ) {
            Configuration.LazyLoadingEnabled=true;
            Configuration.ProxyCreationEnabled=true;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public static ApplicationDbContext Create () {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Club> Clubs {
            get;
            set;
        }

        protected override void OnModelCreating ( DbModelBuilder modelBuilder ) {
            base.OnModelCreating( modelBuilder );
        }
    }
}