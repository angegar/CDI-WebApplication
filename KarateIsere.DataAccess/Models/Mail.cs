using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class Mail {

        /// <summary>
        /// Gets or sets the database email identifier
        /// </summary>
        public int MailId { get; set; }

        /// <summary>
        /// Gets or sets the mail identifier used in the code
        /// </summary>
        [Required(ErrorMessage = "Le message doit avoir un identifieur")]
        public string CommonIdentifer { get; set; }

        /// <summary>
        /// Gets or sets the email message
        /// /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the mail header(logo+CDI)
        /// </summary>
        public Mail Header { get; set; }

        /// <summary>
        /// Gets or sets the help message that will help to understand
        /// how to create a custom mail
        /// </summary>
        public string Help { get; set; }

        public Mail() {

        }

        public Mail(string commonIdentifier) {
            this.CommonIdentifer = commonIdentifier;           
        }
    }
}
