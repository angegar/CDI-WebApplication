using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class Art_Martial {

        /// <summary>
        /// Returns all the Art Martial from the database
        /// </summary>
        /// <returns>A list containing all the databse object</returns>
        public static List<Art_Martial> GetAll(){
            using(KarateIsereContext context = new KarateIsereContext()){
                return context.Art_Martial.ToList();
            }
        }
    }
}
