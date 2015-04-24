//http://msdn.microsoft.com/en-us/data/jj591620.aspx
//https://msdn.microsoft.com/en-us/data/jj591620.aspx
namespace KarateIsere.DataAccess {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using NLog;

    public partial class KarateIsereContext : DbContext {
        Logger log = LogManager.GetCurrentClassLogger();
        public KarateIsereContext()
            : base("KarateIsere") {
            Database.Log = s => log.Trace(s);//System.Diagnostics.Debug.WriteLine( s );
            Configuration.AutoDetectChangesEnabled = true;
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
        public virtual DbSet<Professeurs> Professeurs {
            get;
            set;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            #region Adherents
            modelBuilder.Entity<Adherents>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Grade)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Diplome)
                .IsUnicode(false);

            modelBuilder.Entity<Adherents>()
                .Property(e => e.Num_Affiliation)
                .IsUnicode(false);
            #endregion

            #region admin
            modelBuilder.Entity<Admin>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.PasswordQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.PasswordAnswer)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Prenom)
                .IsUnicode(false);
            #endregion

            #region ArtMartial
            modelBuilder.Entity<Art_Martial>()
                .Property(e => e.Name)
                .IsUnicode(false);
            /*modelBuilder.Entity<Art_Martial>()
                .HasMany( e => e.Club )
                .WithOptional( e => e.Art_Martial1 )
                .HasForeignKey( e => e.Art_Martial )
                .WillCascadeOnDelete();
            */
            #endregion

            #region Categorie
            modelBuilder.Entity<Categorie>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            #endregion

            #region Club
            modelBuilder.Entity<Club>()
                .Property(e => e.NumAffiliation)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.NomClub)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Correspondant)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Adr_Administrative)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Adr_Dojo)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Code_Postal)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Ville)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Site_Web)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.President_Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.President_Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.President_Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.President_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.President_Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Tresorier_Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Tresorier_Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Tresorier_Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Tresorier_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Tresorier_Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Secretaire_Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Secretaire_Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Secretaire_Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Secretaire_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.Secretaire_Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .Property(e => e.ArtMartialName)
                .IsUnicode(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.Competiteur)
                .WithRequired(e => e.Club)
                .HasForeignKey(e => e.NumAffiliationClub);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.ListeCompetiteur)
                .WithRequired(e => e.Club)
                .HasForeignKey(e => e.NumAffiliationClub);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.Professeurs)
                .WithMany(e => e.Club)
                .Map(m => m.ToTable("Club_Professeur").MapLeftKey("NumAffiliation").MapRightKey("ProfesseurId"));
            #endregion

            modelBuilder.Entity<Competition>()
                   .HasMany(e => e.Categorie)
                   .WithMany(e => e.Competitions)
                   .Map(m => m.ToTable("CompetitionCategories")
                       .MapRightKey("Categorie_Nom")
                       .MapLeftKey("Competition_Id"));

            #region Competiteur
            modelBuilder.Entity<Competiteur>()
                .Property(e => e.NumLicence)
                .IsUnicode(false);

            modelBuilder.Entity<Competiteur>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Competiteur>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Competiteur>()
                .Property(e => e.Poids)
                .IsUnicode(false);

            modelBuilder.Entity<Competiteur>()
                .Property(e => e.NumAffiliationClub)
                .IsUnicode(false);

            modelBuilder.Entity<Competiteur>()
                .HasMany(e => e.ListeCompetiteur)
                .WithRequired(e => e.Competiteur)
                .WillCascadeOnDelete(false);

            #endregion


            #region Creneaux
            modelBuilder.Entity<Creneaux>()
                .Property(e => e.NomClub)
                .IsUnicode(false);

            modelBuilder.Entity<Creneaux>()
                .Property(e => e.Jour)
                .IsUnicode(false);

            modelBuilder.Entity<Creneaux>()
                .Property(e => e.HeureDebut)
                .IsUnicode(false);

            modelBuilder.Entity<Creneaux>()
                .Property(e => e.HeureFin)
                .IsUnicode(false);

            modelBuilder.Entity<Creneaux>()
                .Property(e => e.Description)
                .IsUnicode(false);

            #endregion


            modelBuilder.Entity<ListeCompetiteur>()
                .Property(e => e.NomListe)
                .IsUnicode(false);

            modelBuilder.Entity<ListeCompetiteur>()
                .Property(e => e.NumAffiliationClub)
                .IsUnicode(false);

            modelBuilder.Entity<ListeCompetiteur>()
                .Property(e => e.NumLicence)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Grade)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Diplome)
                .IsUnicode(false);

            modelBuilder.Entity<Professeurs>()
                .Property(e => e.Telephone)
                .IsUnicode(false);
        }
    }
}
