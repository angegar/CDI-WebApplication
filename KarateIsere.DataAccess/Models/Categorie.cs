namespace KarateIsere.DataAccess {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Categorie")]
    public partial class Categorie {
        public Categorie() {
            Competiteurs = new HashSet<Competiteur>();
            Competitions = new HashSet<Competition>();
        }

        public int CategorieID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom de la catégorie ne doit pas être nul")]
        [StringLength(50)]
        public string Nom { get; set; }

        public virtual ICollection<Competiteur> Competiteurs { get; set; }

        public virtual ICollection<Competition> Competitions { get; set; }
    }
}
