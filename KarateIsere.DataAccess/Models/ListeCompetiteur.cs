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
        [Key]
        public int Liste_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NomListe { get; set; }

        [Required]
        [StringLength(50)]
        public string NumAffiliationClub { get; set; }

        [Required]
        [StringLength(50)]
        public string NumLicence { get; set; }

        public virtual Club Club { get; set; }

        public virtual Competiteur Competiteur { get; set; }
    }
}
