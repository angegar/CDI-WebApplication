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
        public static List<Art_Martial> GetAll() {
            using (KarateIsereContext context = new KarateIsereContext()) {
                return context.Art_Martial.ToList();
            }
        }

        /// <summary>
        /// Returns all the Art Martial from the database
        /// </summary>
        /// <returns>A list containing all the databse object</returns>
        public static Art_Martial GetById(int art_MartialID, KarateIsereContext context = null) {
            Art_Martial res = null;

            bool closeContext = false;
            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = context.Art_Martial.Where(d => d.Art_MartialID == art_MartialID).SingleOrDefault();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }
    }
}
