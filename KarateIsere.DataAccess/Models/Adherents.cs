namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Adherents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AdherentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Tel { get; set; }

        [StringLength(10)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Grade { get; set; }

        [StringLength(50)]
        public string Diplome { get; set; }

        public bool? IsJugeGrade { get; set; }

        public bool? IsArbitreDept { get; set; }

        public bool? IsArbitreLig { get; set; }

        public bool? IsArbitreNat { get; set; }

        public bool? IsProfesseur { get; set; }

        [StringLength(50)]
        public string NumLicence { get; set; }

        public string NumAffiliation { get; set; }

        public virtual Club Club { get; set; }

        public int CategorieID { get; set; }

        public virtual Categorie Categorie { get; set; }
    }
}
