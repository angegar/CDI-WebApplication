using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class NotificationEmail {

        /// <summary>
        /// Identifiant de la notification
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationEmailID { get; set; }

        /// <summary>
        /// Date de création de l'entrée
        /// </summary>
        public DateTime CreationDate {
            get;
            set;
        }

        /// <summary>
        /// Date de dernière modification
        /// </summary>
        public DateTime LastModif {
            get;
            set;
        }

        /// <summary>
        /// Status de la notification (create|pending|sent|failed)
        /// </summary>
        public string Status {
            get;
            set;
        }

        /// <summary>
        /// Message à envoyer
        /// </summary>
        public string Message { 
            get; 
            set; 
        }

        /// <summary>
        /// Destinataire email
        /// </summary>
        public string Email { 
            get; 
            set; 
        }

        /// <summary>
        /// Competition primary key
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// Compétition pour laquelle on envoie la notification
        /// </summary>
        public virtual Competition Competition { 
            get; 
            set; 
        }

        public string NumAffiliation { get; set; }

        public virtual Club Club { get; set; }

    }
}
