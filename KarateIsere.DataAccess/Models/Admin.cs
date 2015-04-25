namespace KarateIsere.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Key]
        [StringLength(100)]
        public string Login { get; set; }

        [Required]
        [StringLength(1000)]
        public string Password { get; set; }

        [StringLength(1000)]
        public string PasswordQuestion { get; set; }

        [StringLength(1000)]
        public string PasswordAnswer { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        public DateTime? LastAccess { get; set; }
    }
}
