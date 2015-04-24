using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KarateIsere.Models;

 namespace KarateIsere.DataAccess {
     public partial class Club {
         public virtual ApplicationUser AppUser {
             get;
             set;
         }
     }
}