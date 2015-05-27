namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Professeurs
    {
        public Professeurs()
        {
        }

        public int ProfesseursID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Mobile { get; set; }

        [StringLength(50)]
        public string Grade { get; set; }

        public bool? IsJugeGrade { get; set; }

        public bool? IsArbitreDept { get; set; }

        public bool? IsArbitreLigue { get; set; }

        public bool? IsArbitreNational { get; set; }

        [Required]
        [StringLength(50)]
        public string Diplome { get; set; }

        [StringLength(10)]
        public string Telephone { get; set; }

        public string NumAffiliation { get; set; }

        public virtual Club Club { get; set; }
    }
}
