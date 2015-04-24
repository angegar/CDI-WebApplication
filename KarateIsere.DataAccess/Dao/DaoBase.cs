using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess = KarateIsere.DataAccess;

namespace KarateIsere.DataAccess {
    public abstract class DaoBase : Dao, IDisposable {
        protected DataAccess.KarateIsereContext context = new KarateIsereContext();

        public abstract void Create ();

        public abstract void Delete ();

        public abstract void Update ();

        public void Dispose () {
            if (context != null) {
                context.Dispose();
            }
        }

        protected virtual void Delete ( object obj, DbSet set ) {
            //L'entité obj n'existe pas dans le contexte
            //avant l'opération delete c'est pourquoi nous sommes
            //obligés de l'attacher au context et de changer son état.
            //Ceci vient du fait que les méthodes statiques "Get" ne partage pas
            //le contexte de l'instance
            set.Attach( obj );
            context.Entry( obj ).State = EntityState.Deleted;
            context.SaveChanges();
        }
    }
}