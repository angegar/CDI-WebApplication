namespace KarateIsere.DataAccess
{
    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using KarateIsere.Models;
using Newtonsoft.Json;

    [Table("Club")]
    public partial class Club
    {
        public Club()
        {
            Competiteur = new HashSet<Competiteur>();
            ListeCompetiteur = new HashSet<ListeCompetiteur>();
            Professeurs = new HashSet<Professeurs>();
           // ApplicationUser =  new HashSet<ApplicationUser>();
        }

        [Key] 
        [StringLength(50)]
        public string NumAffiliation { get; set; }
       
        [Required]
        [StringLength(100)]
        public string NomClub { get; set; }

        [StringLength(50)]
        [Description("Email de contact")]
        public string Correspondant { get; set; }

        [StringLength(50)]
        public string Telephone { get; set; }

        [StringLength(500)]
        [DisplayName("Adresse postale")]
        public string Adr_Administrative { get; set; }

        [StringLength(500)]
        [DisplayName( "Adresse du dojo" )]
        public string Adr_Dojo { get; set; }

        [StringLength(50)]
        [DisplayName( "Code postal" )]
        public string Code_Postal { get; set; }

        [StringLength(50)]
        public string Ville { get; set; }

        [StringLength(100)]
        [DisplayName( "Site web" )]
        public string Site_Web { get; set; }

        [StringLength(100)]
        public string Facebook { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. fixe" )]
        public string President_Tel { get; set; }

        [StringLength(50)]
        [DisplayName( "Nom" )]
        public string President_Nom { get; set; }

        [StringLength(50)]
        [DisplayName( "Prénom" )]
        public string President_Prenom { get; set; }

        [StringLength(100)]
        [DisplayName( "Email" )]
        public string President_Email { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. mobile" )]
        public string President_Mobile { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. fixe" )]
        public string Tresorier_Tel { get; set; }

        [StringLength(50)]
        [DisplayName( "Nom" )]
        public string Tresorier_Nom { get; set; }

        [StringLength(50)]
        [DisplayName( "Prénom" )]
        public string Tresorier_Prenom { get; set; }

        [StringLength(100)]
        [DisplayName( "Email" )]
        public string Tresorier_Email { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. mobile" )]
        public string Tresorier_Mobile { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. fixe" )]
        public string Secretaire_Tel { get; set; }

        [StringLength(50)]
        [DisplayName( "Nom" )]
        public string Secretaire_Nom { get; set; }

        [StringLength(50)]
        [DisplayName( "Prénom" )]
        public string Secretaire_Prenom { get; set; }

        [StringLength(100)]
        [DisplayName( "Email" )]
        public string Secretaire_Email { get; set; }

        [StringLength(10)]
        [DisplayName( "Tél. mobile" )]
        public string Secretaire_Mobile { get; set; }

        [StringLength(50)]
        [ForeignKey( "Art_Martial" )]
        [DisplayName( "Art martial" )]
        public virtual string ArtMartialName { get; set; }

        public Double? Longitude { get; set; }

        public Double? Latitude {
            get;
            set;
        }
     
         [JsonIgnore]
        public virtual Art_Martial Art_Martial { get; set; }

         [JsonIgnore]
        public virtual ICollection<Competiteur> Competiteur { get; set; }

        public virtual ICollection<ListeCompetiteur> ListeCompetiteur { get; set; }

        public virtual ICollection<Professeurs> Professeurs { get; set; }
    }
}
