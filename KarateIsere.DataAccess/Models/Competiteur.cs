namespace KarateIsere.DataAccess {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Competiteur")]
    public partial class Competiteur {
        public Competiteur() {
            ListeCompetiteur = new HashSet<ListeCompetiteur>();
        }

        [Key]
        [Required]
        [StringLength(50)]
        public string NumLicence {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string Nom {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string Prenom {
            get;
            set;
        }

        [Required]
        public int CategorieID { get; set; }

        [Required]
        [StringLength(50)]
        public string Poids {
            get;
            set;
        }

        [Display(Name = "Homme")]
        public bool isHomme {
            get;
            set;
        }

        [Required]
        [StringLength(50)]
        public string NumAffiliation {
            get;
            set;
        }

        [Required]
        public virtual Categorie Categorie {
            get;
            set;
        }

        public virtual Club Club {
            get;
            set;
        }

        public virtual ICollection<ListeCompetiteur> ListeCompetiteur {
            get;
            set;
        }

        public virtual ICollection<Inscriptions> Inscriptions {
            get;
            set;
        }
    }
}
