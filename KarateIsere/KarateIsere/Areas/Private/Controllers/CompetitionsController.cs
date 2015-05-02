using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.DataAccess.Tool;
using KarateIsere.Models;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles = "Admin")]
    public class CompetitionsController : Controller {
        // GET: Private/Competitions
        public ActionResult Index() {
            return View(GetCompetitions());
        }

        // GET: Private/Competitions/Details/5
        public ActionResult Details(int id) {
            List<Competiteur> c = Inscriptions.GetInscriptions(id);
            ViewBag.CompetId = id;
            return View(c);
        }

        /// <summary>
        /// Exporte une compétition vers excel
        /// </summary>
        /// <param name="id">Compétition Id</param>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult Export(int id) {
            Competition compet = Competition.GetById(id);
            List<Competiteur> competiteurs = Inscriptions.GetInscriptions(id);
            string delimChar = ";";
            //Si je ferme moi-même les stream l'application crash
            //il semble que le framework les ferme lui-même
            MemoryStream output = new MemoryStream();
            StreamWriter writer = new StreamWriter(output, Encoding.UTF8);

            //headers
            writer.Write("Club");
            writer.Write(delimChar);
            writer.Write("Catégorie");
            writer.Write(delimChar);
            writer.Write("NumLicence");
            writer.Write(delimChar);
            writer.Write("Nom");
            writer.Write(delimChar);
            writer.Write("Prenom");
            writer.Write(delimChar);
            writer.Write("Poids");
            writer.WriteLine();

            //Content
            foreach (Competiteur c in competiteurs) {
                writer.Write(c.Club.NomClub);
                writer.Write(delimChar);
                writer.Write(c.CategorieName);
                writer.Write(delimChar);
                writer.Write(c.NumLicence);
                writer.Write(delimChar);
                writer.Write(c.Nom);
                writer.Write(delimChar);
                writer.Write(c.Prenom);
                writer.Write(delimChar);
                writer.Write(c.Poids);
                writer.WriteLine();
            }

            writer.Flush();
            output.Position = 0;

            return this.File(output,
                            "application/vnd.ms-excel",
                             compet.Nom + "_" + compet.DateCompetition.ToShortDateString() + ".csv");
        }

        [HttpPost]
        public ActionResult Create(Competition compet) {
            DateTime competDate = compet.DateCompetition;
            DateTime finInscription = compet.FinInscription;

            if (competDate.CompareTo(finInscription) <= 0) {
                ModelState.AddModelError("DateCompetition",
                                    "La date de la compétition doit être " +
                                    "supérieur à la date de fin d'inscription");
            }

            if (ModelState.IsValid) {
                compet.Categorie = (List<Categorie>) Session["selectedCategorie"];
                compet.Create();
                Session.Remove("selectedCategorie");
            }

            return View("Index", GetCompetitions());
        }

        // GET: Private/Competitions/Edit/5
        public ActionResult Edit(string competName) {
            Competition c = Competition.GetByName(competName);

            //Liste des catégories déjà enregistrées
            List<Categorie> listCate = c.Categorie.ToList();
            Session["selectedCategorie"] = listCate;
            List<string> cate = GetCateList(listCate);
            ViewBag.CateList = cate;

            return View(c);
        }

        // POST: Private/Competitions/Edit/5
        [HttpPost]
        public ActionResult Edit(Competition compet) {
            try {
                if (ModelState.IsValid) {
                    compet.Categorie = (List<Categorie>) Session["selectedCategorie"];
                    compet.Update();
                    Session.Remove("selectedCategorie");
                    return View("Index", GetCompetitions());
                }

                return View("Edit", compet);
            }
            catch (Exception e) {
                return View();
            }
        }

        public ActionResult Delete(string competName) {
            Competition c = Competition.GetByName(competName);

            if (c != null) {
                c.Delete();
            }

            return View("Index", GetCompetitions());
        }

        /// <summary>
        /// Ajoute une catégorie dans la variable de session qui sera
        /// utilisée pour la création de la compétition
        /// </summary>
        /// <param name="name">Catégorie à ajouter</param>
        public void AddCategorie(string name) {
            List<Categorie> selectedCate;

            if (Session["selectedCategorie"] != null) {
                selectedCate = (List<Categorie>) Session["selectedCategorie"];
            }
            else {
                selectedCate = new List<Categorie>();
            }

            Categorie c = new Categorie {
                Nom = name
            };

            selectedCate.Add(c);
            Session["selectedCategorie"] = selectedCate;
        }

        /// <summary>
        /// Supprime une catégorie dans la variable de session qui sera
        /// utilisée pour la création de la compétition
        /// </summary>
        /// <param name="name">Catégorie à supprimer</param>
        public void DelCategorie(string name, string competName) {
            Contract.Requires(Session["selectedCategorie"] != null,
                               "La variable de session ne devrait pas être vide à cet endroit du code");

            List<Categorie> selectedCate;

            if (Session["selectedCategorie"] != null) {
                selectedCate = (List<Categorie>) Session["selectedCategorie"];
            }
            else {
                //Ne devrait jamais se produire
                Competition c = Competition.GetByName(competName);
                selectedCate = c.Categorie.ToList();
            }

            Categorie cate = selectedCate.Where(d => d.Nom == name).SingleOrDefault();
            if (cate != null) {
                selectedCate.Remove(cate);
            }

            Session["selectedCategorie"] = selectedCate;
        }

        private List<Competition> GetCompetitions() {
            //Used to init the Categorie dropdown list
            ViewBag.CateList = GetCateList();
            List<Competition> compets = Competition.GetAll();
            return compets;

        }

        /// <summary>
        /// Récupère la liste des catégories pour les afficher
        /// </summary>
        /// <param name="excluded">Catégories déjà sélectionnées
        /// à exclure de la liste</param>
        /// <returns></returns>
        private List<string> GetCateList(List<Categorie> excluded = null) {
            List<String> cateList = Categorie.GetAll().Select(d => d.Nom).ToList();

            if (excluded != null) {
                List<string> selectedCate = excluded.Select(d => d.Nom).ToList();
                cateList.RemoveAll(d => selectedCate.Contains(d));
            }

            return cateList;
        }

        #region Json Reporting
        /// <summary>
        /// Retourne le nombre de participant par catégorie d'age
        /// </summary>
        /// <param name="id">Compétition id</param>
        /// <returns></returns>
        public JsonResult PieCategorie(int id) {
            //Competition compet = Competition.GetById(id);
            List<Competiteur> competiteurs = Inscriptions.GetInscriptions(id);
            var res = from c in competiteurs
                      group c by c.CategorieName into g
                      select new { Categorie = g.Key, Count = g.Count() };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retourne le nombre de participant par catégorie d'age
        /// </summary>
        /// <param name="id">Compétition id</param>
        /// <returns></returns>
        public JsonResult PieSexe(int id) {
            //Competition compet = Competition.GetById(id);
            List<Competiteur> competiteurs = Inscriptions.GetInscriptions(id);
            var res = from c in competiteurs
                      group c by c.isHomme into g
                      select new { Sexe = g.Key, Count = g.Count() };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
