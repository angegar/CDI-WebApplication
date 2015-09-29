using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateIsere.DataAccess {
    public partial class Inscriptions : DaoBase {
        public override void Create() {
            Contract.Requires(!string.IsNullOrWhiteSpace(NumLicence));
            Contract.Requires(CompetitionID > 0);

            bool existInscr = context.Inscriptions.Any(d =>
                d.CompetitionID == CompetitionID &&
                d.NumLicence == NumLicence);


            //Inscriptions c = context.Inscriptions.Find( InscriptionId );
            if (!existInscr) {
                //Competiteur = Competiteur.Get(NumLicence, context);
                //Competition = Competition.GetById(CompetitionID, context);

                context.Inscriptions.Add(this);
                context.SaveChanges();
            }
            else {
                throw new Exception("Ce compétiteur est déjà inscrit.");
            }

        }

        public override void Delete() {
            Inscriptions i = null;
            if (InscriptionId == 0) {
                if (CompetitionID > 0 && !string.IsNullOrWhiteSpace(NumLicence)) {
                    i = GetInscriptions(CompetitionID, NumLicence);
                }
            }
            else {
                i = context.Inscriptions.Find(InscriptionId);
            }

            if (i != null) {
                context.Inscriptions.Remove(i);
                context.SaveChanges();
            }
            else {
                throw new Exception("Impossible de supprimé cette inscription car elle n'existe pas");
            }
        }

        /// <summary>
        /// Retoure un inscription à partir de l'id de la compétition et du numéro de licence
        /// de l'adhérent
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="numLicence"></param>
        /// <returns>Un inscription ou null</returns>
        public Inscriptions GetInscriptions(int competitionId, string numLicence) {
            return (from i in context.Inscriptions
                    where i.CompetitionID == competitionId && i.NumLicence == numLicence
                    select i).SingleOrDefault();
        }

        public override void Update() {
            Inscriptions i = context.Inscriptions.Find(InscriptionId);
            if (i != null) {
                context.Entry(i).CurrentValues.SetValues(this);
                context.SaveChanges();
            }
            else {
                throw new Exception("Impossible de mettre à jour cette inscription car elle n'existe pas");
            }
        }

        public static List<Inscriptions> GetClubInscriptions(int competitionId, string numAffiliation, KarateIsereContext context = null) {
            List<Inscriptions> res = new List<Inscriptions>();
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }
                res = (from i in context.Inscriptions
                       join co in context.Competiteur on i.NumLicence equals co.NumLicence
                       where i.CompetitionID == competitionId && co.NumAffiliation == numAffiliation
                       select i).ToList();


            }
            finally {
                if (closeContext) context.Dispose();
            }
            return res;
        }

        /// <summary>
        /// Retourne la liste des compétiteurs inscrits à une compétition.
        /// Cette methode devrait peut être se trouver dans la classe Competitions
        /// </summary>
        /// <param name="competitionId">Compétitions Ids</param>
        /// <param name="context">Model Context</param>
        /// <returns>Liste des compétiteurs</returns>
        public static List<Competiteur> GetInscriptions(int competitionId, KarateIsereContext context = null) {
            Contract.Requires(competitionId > 0);

            List<Competiteur> res = new List<Competiteur>();
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from i in context.Inscriptions
                       join co in context.Competiteur on i.NumLicence equals co.NumLicence
                       where i.CompetitionID == competitionId
                       select co).Include(d => d.Categorie)
                                 .Include(d => d.Club)
                                 .OrderBy(d => d.Categorie.Nom)
                                 .ThenBy(d => d.Club.NomClub).ToList();


            }
            finally {
                if (closeContext) context.Dispose();
            }
            return res;

        }

        public static List<Club> GetNotInscripts(int competitionId, KarateIsereContext context = null) {
            List<Club> res = new List<Club>();
            bool closeContext = false;
            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                //Liste des clubs déjà inscrits
                List<Club> clubInscrits = (from i in context.Inscriptions
                                           join comp in context.Competiteur on i.NumLicence equals comp.NumLicence
                                           join clubInsc in context.Club on comp.NumAffiliation equals clubInsc.NumAffiliation
                                           where i.CompetitionID == competitionId
                                           select clubInsc).ToList();

                //Liste des clubs ayant déjà participé à une compétition
                List<Club> clubPart = (from i in context.Inscriptions
                                       join c in context.Club on i.Competiteur.Club.NumAffiliation equals c.NumAffiliation
                                       select c).ToList();

                //Liste des club ayant déjà participé à une compétition mais n'étant pas inscrits
                res = (from c in clubPart
                       where !(from c1 in clubInscrits
                               select c1.NumAffiliation).Contains(c.NumAffiliation)
                       select c).ToList();
            }
            finally {
                if (closeContext) context.Dispose();
            }
            return res;
        }
    }
}
