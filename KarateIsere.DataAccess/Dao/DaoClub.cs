using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {

    public partial class Club : DaoBase {
        public override sealed void Create() {
            Club c = Get(NumAffiliation);

            if (c == null) {
                if (Art_Martial == null) {
                    Art_Martial = Art_Martial.GetById(1);  
                }

                context.Club.Add(this);
                context.SaveChanges();
            }
        }

        public override sealed void Delete() {
            Club c = Get(NumAffiliation);

            if (c != null) {
                context.Club.Attach(c);
                Delete(c, context.Club);
            }
        }

        public override sealed void Update() {
            Contract.Requires(!string.IsNullOrWhiteSpace(NumAffiliation),
                               "Le numero d'affiliation doit etre fournit");

            Club c = Get(NumAffiliation);

            if (c != null) {
                Art_Martial = Art_Martial.GetById(art_MartialID, context);
                context.Club.Attach(this);
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public Club ShallowCopy() {
            return (Club) this.MemberwiseClone();
        }

        public static Club GetByNumAffiliation(string numAffiliation) {
            using (KarateIsereContext context = new KarateIsereContext()) {
                Club res = (from c in context.Club
                            where c.NumAffiliation == numAffiliation
                            select c).SingleOrDefault();
                return res;
            }
        }

        public static List<Club> GetAll() {
            List<Club> res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                res = (from c in context.Club
                       select c)
                       .Include(d => d.Art_Martial)
                       .Include(d=>d.Adherents)
                       .Include(d=>d.Competiteur)  
                       .Include(d=>d.ListeCompetiteur)
                       .Include(d => d.Notifications)
                       .Include(d => d.Professeurs)
                       .ToList();
            }

            return res;
        }

        public static Club Get(string numAffiliation) {
            Club res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                context.Configuration.ProxyCreationEnabled = false;
                res = context.Club.Include(d => d.Competiteur)
                                   .Where(d => d.NumAffiliation == numAffiliation).
                                   SingleOrDefault();
            }

            return res;
        }
    }
}