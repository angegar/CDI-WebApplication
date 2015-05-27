namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inscriptions
    {
        [Key]
        public int InscriptionId {
            get;
            set;
        }

        //[Column(Order = 0)]
        [StringLength(50)]
        [ForeignKey("Competiteur")]
        public string NumLicence { get; set; }

        [ForeignKey("Competition")]
        //[Column(Order = 1)]  
        public int CompetitionID { get; set; }

        public int? Classement { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual Competiteur Competiteur { get; set; }
    }
}
