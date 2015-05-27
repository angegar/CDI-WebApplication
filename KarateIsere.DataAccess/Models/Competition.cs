namespace KarateIsere.DataAccess {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table( "Competition" )]
    public partial class Competition {
        public Competition () {
            Inscriptions = new HashSet<Inscriptions>();
            Categorie = new HashSet<Categorie>();
            Notifications = new HashSet<NotificationEmail>();
        }
        
        public int CompetitionID {
            get;
            set;
        }

        [StringLength( 100 )]
        [Required( ErrorMessage="La compétition doit avoir un nom" )]
        public string Nom {
            get;
            set;
        }

        [Column( TypeName = "date" )]
        [DataType( DataType.Date )]
        [DisplayName( "Date de la compétition" )]
        [DisplayFormat( ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}" )]
        [Required( ErrorMessage="La compétition doit avoir une date" )]
        public DateTime DateCompetition {
            get;
            set;
        }

        [Column( TypeName = "date" )]
        [DataType( DataType.Date )]
        [DisplayName( "Date de fin d'inscription" )]
        [DisplayFormat( ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}" )]
        [Required( ErrorMessage="La compétition doit avoir un date de fin d'inscription" )]
        public DateTime FinInscription {
            get;
            set;
        }

        [StringLength( 100 )]
        [Required( ErrorMessage="La compétition doit avoir un lieu" )]
        public string Lieu {
            get;
            set;
        }

        [DisplayName( "Compétition kata" )]
        public bool IsKata {
            get;
            set;
        }

        public virtual ICollection<Inscriptions> Inscriptions {
            get;
            set;
        }

        public virtual ICollection<NotificationEmail> Notifications {
            get;
            set;
        }

        public virtual ICollection<Categorie> Categorie {
            get;
            set;
        }
    }
}
