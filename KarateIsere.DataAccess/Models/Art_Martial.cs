namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Art_Martial
    {
        public Art_Martial()
        {
        }

        [Key]
        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
