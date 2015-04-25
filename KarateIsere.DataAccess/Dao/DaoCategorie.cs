using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarateIsere.DataAccess;

namespace KarateIsere.DataAccess {
    public partial class Categorie : DaoBase {

        public override sealed void Create () {
            Contract.Requires(!string.IsNullOrWhiteSpace(Nom));

            if (Get( Nom ) == null) {
                context.Categorie.Add( this );
                context.SaveChanges();
            }
        }

        public override void Delete () {
            Contract.Requires(!string.IsNullOrWhiteSpace(Nom));
            Categorie c = Get( Nom );
            if (c != null) {       
                Delete( c, context.Categorie );
            }
        }

        public override void Update () {
            Contract.Requires( !string.IsNullOrWhiteSpace( Nom ),
                               "Le nom de la categorie doit etre fournit" );

            if (Get( Nom ) != null) {
                context.Categorie.Attach( this );
                context.Entry( this ).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static List<Categorie> GetAll () {
            List<Categorie> res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                context.Configuration.ProxyCreationEnabled = false;
                res = context.Categorie.ToList();
                context.Configuration.ProxyCreationEnabled = true;
            }

            return res;
        }

        public static Categorie Get ( string cateName ) {
            Contract.Requires(!string.IsNullOrWhiteSpace(cateName));
            Categorie res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                context.Configuration.ProxyCreationEnabled = false;
                res = context.Categorie.Where(d=>d.Nom == cateName).SingleOrDefault();
                context.Configuration.ProxyCreationEnabled = true;
            }

            return res;
        }
    }
}
