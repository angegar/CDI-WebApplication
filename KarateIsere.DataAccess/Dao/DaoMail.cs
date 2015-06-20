using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class Mail : DaoBase {

        /// <summary>
        /// Gets a mail object from its commonIdentifier
        /// </summary>
        /// <param name="mailIdentifier"></param>
        /// <returns></returns>
        public static Mail Get(string commonIdentifier, KarateIsereContext context = null) {
            Mail res = null;
            bool closeContext = false;
            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Mail
                       where c.CommonIdentifer == commonIdentifier
                       select c).SingleOrDefault();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }

        public override void Create() {
            throw new NotImplementedException();
        }

        public override void Delete() {
            throw new NotImplementedException();
        }

        public override void Update() {
            throw new NotImplementedException();
        }
    }
}
