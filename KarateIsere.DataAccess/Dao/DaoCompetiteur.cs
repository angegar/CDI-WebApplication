using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace KarateIsere.DataAccess {
    public partial class Competiteur : DaoBase {

        public override sealed void Create() {
            Categorie c = context.Categorie.Find(CategorieName);
            if (c != null) {
                Categorie = c;
                /* context.Competiteur.Attach( this );
                 context.Entry( this ).State = EntityState.Added;*/
                context.Competiteur.Add(this);
                context.SaveChanges();
            }
            else {
                throw new Exception("Un compétiteur avec le numéro de licence [" + NumLicence + "] existe déjà");
            }
        }

        public override void Delete() {
            Competiteur c = context.Competiteur.Find(NumLicence);
            if (c != null) {
                context.Competiteur.Remove(c);
                context.SaveChanges();
            }
            else {
                throw new Exception("Il n'y a pas de compétiteur avec le numéro de licence [" + NumLicence + "]");
            }
        }

        public override void Update() {
            //http://stackoverflow.com/questions/13236116/entity-framework-problems-updating-related-objects
            Competiteur c = context.Competiteur.
                                      Include(d => d.Categorie).
                                      Where(e => e.NumLicence == NumLicence).
                                      SingleOrDefault();

            if (c != null) {
                if (c.CategorieName != CategorieName) {
                    Categorie = Categorie.Get(CategorieName);
                    context.Categorie.Attach(Categorie);
                }

                context.Entry(c).CurrentValues.SetValues(this);
                context.SaveChanges();
            }
            else {
                throw new Exception("Il n'y a pas de compétiteur avec le numéro de licence [" + NumLicence + "]");
            }
        }
        /// <summary>
        /// Get a competiteur by its numLicence
        /// </summary>
        /// <param name="numLicence">Numéro de licence</param>
        /// <param name="context">KarateIsereContext</param>
        /// <returns>A competiteur or null</returns>
        public static Competiteur Get(string numLicence, KarateIsereContext context = null) {
            Competiteur res = null;
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Competiteur
                       where c.NumLicence.ToUpper() == numLicence.ToUpper()
                       select c).
                                  Include(d => d.Categorie).
                                  Include(d => d.Club).
                                  SingleOrDefault();
            }
            finally {
                if (closeContext) {
                    context.Dispose();
                }

            }

            return res;
        }

        /// <summary>
        /// Get la liste de compétiteur d un club
        /// </summary>
        /// <param name="numAffiliation">Numéro d'affiliation du club</param>
        /// <param name="context">KarateIsereContext</param>
        /// <returns>Liste de compétieur ou une liste vide</returns>
        public static List<Competiteur> GetCompetitieurs(string numAffiliation, KarateIsereContext context = null) {
            List<Competiteur> res = new List<Competiteur>();
            bool closeContext = false;
            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Competiteur
                       where c.NumAffiliationClub == numAffiliation
                       select c).ToList();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }
    }
}
