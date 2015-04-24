namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Creneaux
    {
        [Key]
        public int CreneauId { get; set; }

        [Required]
        [StringLength(100)]
        public string NomClub { get; set; }

        [Required]
        [StringLength(50)]
        public string Jour { get; set; }

        [Required]
        [StringLength(50)]
        public string HeureDebut { get; set; }

        [Required]
        [StringLength(50)]
        public string HeureFin { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }
}
