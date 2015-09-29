using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model = KarateIsere.DataAccess;
namespace KarateIsere.DataAccess {
    public partial class Competition : DaoBase {

        public override void Create() {
            Competition c = GetByName(Nom);
            if (c == null) {
                GetCategorie();
                context.Competition.Add(this);
                context.SaveChanges();
            }
        }

        public override void Delete() {
            //Competition compet = context.Competition.Find(CompetitionID);
            Competition compet = (from c in context.Competition
                                  where CompetitionID == c.CompetitionID
                                  select c).Include(d => d.Categorie).SingleOrDefault();

            if (compet != null) {
                compet.Categorie = null;
                compet.Inscriptions = null;
                context.Competition.Remove(compet);
            }

            context.SaveChanges();
        }

        public override void Update() {
            Competition c = context.Competition.
                                    Include(d => d.Categorie).
                                    Where(e => e.CompetitionID == CompetitionID).
                                    SingleOrDefault();

            c.Nom = Nom;
            c.IsKata = IsKata;
            c.Lieu = Lieu;
            c.DateCompetition = DateCompetition;
            c.FinInscription = FinInscription;

            //context.Entry(c).CurrentValues.SetValues(this);

            //Supprime toutes les catégories
            c.Categorie.Clear();

            //Recrée les catégories
            foreach (Categorie ca in Categorie) {
                Categorie tmp = context.Categorie.Where(
                                    d => d.Nom == ca.Nom
                                    ).SingleOrDefault();
                if (tmp != null) {
                    c.Categorie.Add(tmp);
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Case sensitive search
        /// </summary>
        /// <param name="Nom">Nom de la compétition</param>
        /// <param name="context">KarateIsereContext</param>
        /// <returns>Une compétition ou null</returns>
        public static Competition GetByName(string Nom, KarateIsereContext context = null) {
            Competition res = null;
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Competition
                       where c.Nom.ToUpper() == Nom
                       select c).
                                   Include(d => d.Categorie).
                                   SingleOrDefault();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }

        /// <summary>
        ///Get a competition by id
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="context">KarateIsereContext</param>
        /// <returns>A competition or null</returns>
        public static Competition GetById(int id, KarateIsereContext context = null) {
            Competition res = null;
            bool closeContext = false;
            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Competition
                       where c.CompetitionID == id
                       select c).
                                   Include(d => d.Categorie).
                                   SingleOrDefault();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;

        }

        /// <summary>
        /// Get toutes les compétitions
        /// </summary>
        /// <param name="context">KaratéIsereContext</param>
        /// <returns>Liste des compétitions ou null</returns>
        public static List<Competition> GetAll(KarateIsereContext context = null) {
            List<Competition> res = new List<Competition>();
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = context.Competition.ToList();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }

        /// <summary>
        /// Récupère la liste des compétitions auxquelles
        /// les clubs peuvent encore s'inscrire
        /// </summary>
        /// <returns></returns>
        public static List<Competition> GetActive(KarateIsereContext context = null) {
            List<Competition> res = new List<Competition>();
            bool closeContext = false;

            try {
                if (context == null) {
                    context = new KarateIsereContext();
                    closeContext = true;
                }

                res = (from c in context.Competition
                       where c.FinInscription.CompareTo(DateTime.Now) >= 0
                       select c).ToList();
            }
            finally {
                if (closeContext) context.Dispose();
            }

            return res;
        }

        private void GetCategorie() {
            if (Categorie != null) {
                List<Categorie> ctmp = new List<Categorie>();
                foreach (Categorie ca in Categorie) {
                    Categorie tmp = Model.Categorie.Get(ca.Nom, context);

                    if (tmp != null) {
                        ctmp.Add(tmp);
                    }
                    else {
                        throw new Exception("La catégorie n'existe pas");
                    }
                }

                Categorie = ctmp;
            }
        }
    }
}
