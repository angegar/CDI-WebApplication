namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ListeCompetiteur")]
    public partial class ListeCompetiteur
    {

        public ListeCompetiteur() {
            Competiteurs = new HashSet<Competiteur>();
        }
        public int ListeCompetiteurID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string NumAffiliation { get; set; }

        public virtual Club Club { get; set; }

        public virtual ICollection<Competiteur> Competiteurs { get; set; }
    }
}
