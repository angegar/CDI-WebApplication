using KarateIsere.DataAccess;
using KarateIsere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles="User")]
    public class InscriptionsController : Controller {
        // GET: Private/Inscriptions
        public ActionResult Index() {
            ApplicationUser club = (ApplicationUser) Session["User"];
            ViewBag.Competition = ToSelectItem(Competition.GetActive());

            List<Competiteur> dejaInscr = new List<Competiteur>();
            List<Competiteur> competiteur = new List<Competiteur>();

            if (Session["selectedCompetition"] != null) {
                int competitionId = Convert.ToInt32(Session["selectedCompetition"]);
                dejaInscr = GetInscrits(club, competitionId);
                competiteur = GetCompetiteurs(club.NumAffiliation, dejaInscr);
            }
            else {
                competiteur = Competiteur.GetCompetitieurs(club.NumAffiliation);
            }

            ViewBag.Inscrits = dejaInscr;
            Session["SelectedCompetiteurs"] = dejaInscr.Select(d => d.NumLicence).ToList();
            ViewBag.Competiteurs = competiteur;

            return View();
        }

        /// <summary>
        /// Retourne la liste de inscrits à la compétition
        /// </summary>
        /// <param name="club">Numéro d'affiliation du club</param>
        /// <param name="competitionId">Compétition Id</param>
        /// <returns>La liste des inscrits ou une liste vide</returns>
        private List<Competiteur> GetInscrits(ApplicationUser club, int competitionId) {
            List<Competiteur> inscrits = new List<Competiteur>();
            List<Inscriptions> inscr = Inscriptions.GetClubInscriptions(competitionId, club.NumAffiliation);

            //Get les compétieurs à partir de leur numéro de licence
            foreach (Inscriptions i in inscr) {
                inscrits.Add(Competiteur.Get(i.NumLicence));
            }

            return inscrits;
        }

        /// <summary>
        /// Retourne la liste des compétiteurs n'étant pas inscrits à la compétition
        /// </summary>
        /// <param name="numAffClub">Numéro d'affiliation du club</param>
        /// <param name="exclude">Liste des compétiteurs déjà inscrits</param>
        /// <returns>Liste de compétiteur ou null</returns>
        private List<Competiteur> GetCompetiteurs(string numAffClub, List<Competiteur> exclude) {
            List<Competiteur> res = Competiteur.GetCompetitieurs(numAffClub);
            res.RemoveAll(d => exclude.Any(
                                e => e.NumLicence == d.NumLicence
                                )
                         );
            return res;
        }

        private List<SelectListItem> ToSelectItem(List<Competition> list) {
            List<SelectListItem> res = new List<SelectListItem>();
            foreach (Competition c in list) {
                SelectListItem sli = new SelectListItem {
                    Text = c.Nom,
                    Value = c.Id.ToString()
                };

                if (Session["selectedCompetition"] != null &&
                   Convert.ToInt32(Session["selectedCompetition"]) == c.Id) {
                    sli.Selected = true;
                }

                res.Add(sli);
            }

            if (Session["selectedCompetition"] == null && res.Count > 0) {
                SelectListItem first = res.First();
                first.Selected = true;
                Session["selectedCompetition"] = first.Value;
            }

            return res;
        }

        public void AddCompetiteur(string numLicence) {
            List<string> selLicences;
            if (Session["SelectedCompetiteurs"] != null) {
                selLicences = (List<string>) Session["SelectedCompetiteurs"];
            }
            else {
                selLicences = new List<string>();
            }

            selLicences.Add(numLicence);
            Session.Add("SelectedCompetiteurs", selLicences);
        }

        public void DelCompetiteur(string numLicence) {
            List<string> selLicences;
            if (Session["SelectedCompetiteurs"] != null) {
                selLicences = (List<string>) Session["SelectedCompetiteurs"];
            }
            else {
                selLicences = new List<string>();
            }

            selLicences.Remove(numLicence);
            Session.Add("SelectedCompetiteurs", selLicences);
        }

        public void SetCompetition(string competId) {
            Session["selectedCompetition"] = competId;
        }

        // GET: Private/Inscriptions/Create
        public ActionResult Create() {
            //ViewBag.Competition = ToSelectItem(Competition.GetActive());
            return View();
        }

        // POST: Private/Inscriptions/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                ApplicationUser club = (ApplicationUser) Session["User"];
                int competitionId = Convert.ToInt32(collection["CompetitionId"]);
                List<string> selCompetiteurs = null;
                List<Inscriptions> inscriptions = new List<Inscriptions>();
                List<Competiteur> dejaInscr = GetInscrits(club, competitionId);

                if (Session["SelectedCompetiteurs"] != null) {
                    selCompetiteurs = (List<string>) Session["SelectedCompetiteurs"];

                    //Create inscription
                    foreach (string numAff in selCompetiteurs) {
                        Inscriptions i = new Inscriptions();
                        i.CompetitionId = competitionId;
                        i.NumLicence = numAff;

                        //Si l'inscription n'existe pas alors la créé
                        if (!dejaInscr.Any(d => d.NumLicence == numAff)) {
                            i.Create();
                        }
                    }

                    //Delete inscription
                    foreach (Competiteur c in dejaInscr) {
                        if (!selCompetiteurs.Contains(c.NumLicence)) {
                            Inscriptions i = new Inscriptions();
                            i.CompetitionId = competitionId;
                            i.NumLicence = c.NumLicence;
                            i.Delete();
                        }
                    }
                }

                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                return View();
            }
        }
    }
}
