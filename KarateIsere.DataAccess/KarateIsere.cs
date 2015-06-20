//http://msdn.microsoft.com/en-us/data/jj591620.aspx
//https://msdn.microsoft.com/en-us/data/jj591620.aspx
namespace KarateIsere.DataAccess {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using NLog;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using System.Security.Claims;
    using System.Collections.Generic;

    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public partial class ApplicationUser : IdentityUser {

        public ApplicationUser() {

        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager) {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

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

    public partial class KarateIsereContext : IdentityDbContext<ApplicationUser> {
        Logger log = LogManager.GetCurrentClassLogger();

        public static KarateIsereContext Create() {
            return new KarateIsereContext();
        }

        public KarateIsereContext()
            : base("KarateIsere", throwIfV1Schema: false) {
            Database.Log = s => log.Trace(s);//System.Diagnostics.Debug.WriteLine( s );
            Configuration.AutoDetectChangesEnabled = true;

            //Database.SetInitializer<KarateIsereContext>(new KarateIsereDBInitializer());

            //Database.SetInitializer<KarateIsereContext>(new CreateDatabaseIfNotExists<KarateIsereContext>());

            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseIfModelChanges<SchoolDBContext>());
            //Database.SetInitializer<SchoolDBContext>(new DropCreateDatabaseAlways<SchoolDBContext>());
        }


        public virtual DbSet<Adherents> Adherents {
            get;
            set;
        }
        public virtual DbSet<Admin> Admin {
            get;
            set;
        }
        public virtual DbSet<Art_Martial> Art_Martial {
            get;
            set;
        }
        public virtual DbSet<Categorie> Categorie {
            get;
            set;
        }
        public virtual DbSet<Club> Club {
            get;
            set;
        }
        public virtual DbSet<Competiteur> Competiteur {
            get;
            set;
        }
        public virtual DbSet<Competition> Competition {
            get;
            set;
        }
        public virtual DbSet<Creneaux> Creneaux {
            get;
            set;
        }
        public virtual DbSet<Inscriptions> Inscriptions {
            get;
            set;
        }
        public virtual DbSet<ListeCompetiteur> ListeCompetiteur {
            get;
            set;
        }
        public virtual DbSet<NotificationEmail> NotificationEmail {
            get;
            set;
        }
        public virtual DbSet<Mail> Mail {
            get;
            set;
        }
        public virtual DbSet<Professeurs> Professeurs {
            get;
            set;
        }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Lien N-N entre catégorie et compétitions
            modelBuilder.Entity<Competition>()
                   .HasMany(e => e.Categorie)
                   .WithMany(e => e.Competitions)
                   .Map(m => m.ToTable("CompetitionCategories")
                       .MapRightKey("CategorieID")
                       .MapLeftKey("CompetitionID"));

            //Lien N-N entre liste compétiteur et compétiteurs
            modelBuilder.Entity<ListeCompetiteur>()
                   .HasMany(e => e.Competiteurs)
                   .WithMany(e => e.ListeCompetiteur)
                   .Map(m => m.ToTable("ListeCompetiteursCompetiteurs")
                       .MapRightKey("ListeCompetiteurID")
                       .MapLeftKey("NumLicence"));

            //Lien 1-N entre compétition et notification
            modelBuilder.Entity<Competition>()
                    .HasMany(e => e.Notifications)
                    .WithRequired(e => e.Competition)
                    .WillCascadeOnDelete(true);

            //Lien 1-N entre Compétiteur et Inscriptions
            modelBuilder.Entity<Competiteur>()
                .HasMany(e => e.Inscriptions)
                .WithRequired(e => e.Competiteur)
                .WillCascadeOnDelete(true);

            //Lien 1-N entre catégorie et compétiteur
            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Competiteurs)
                .WithRequired(e => e.Categorie)
                .WillCascadeOnDelete(true);

            //Lien 1-N entre club et compétiteur
            modelBuilder.Entity<Club>()
               .HasMany(e => e.Competiteur)
               .WithRequired(e => e.Club)
               .WillCascadeOnDelete(true);
        }
    }

    public class KarateIsereDBInitializer : DropCreateDatabaseAlways<KarateIsereContext> {
        protected override void Seed(KarateIsereContext context) {
            #region Init Art Martiaux
            IList<Art_Martial> defaultStandards = new List<Art_Martial>();
            defaultStandards.Add(new Art_Martial() { Name = "Karaté Shotokan" });
            defaultStandards.Add(new Art_Martial() { Name = "Karaté Shito ryu" });
            defaultStandards.Add(new Art_Martial() { Name = "karaté Wado ryu" });
            defaultStandards.Add(new Art_Martial() { Name = "karaté Goju ryu" });
            defaultStandards.Add(new Art_Martial() { Name = "karaté Contact" });

            foreach (Art_Martial std in defaultStandards)
                context.Art_Martial.Add(std);
            #endregion

            #region Init Catégorie
            IList<Categorie> categories = new List<Categorie>();
            categories.Add(new Categorie() { Nom = "Poussin" });
            categories.Add(new Categorie() { Nom = "Pupille" });
            categories.Add(new Categorie() { Nom = "Benjamin" });
            categories.Add(new Categorie() { Nom = "Minime" });
            categories.Add(new Categorie() { Nom = "Cadet" });
            categories.Add(new Categorie() { Nom = "Junior" });
            categories.Add(new Categorie() { Nom = "Sénior" });

            foreach (Categorie c in categories)
                context.Categorie.Add(c);
            #endregion

            base.Seed(context);
        }
    }
}
