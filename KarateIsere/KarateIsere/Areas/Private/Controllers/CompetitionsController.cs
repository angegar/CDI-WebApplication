using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.DataAccess.Tool;
using KarateIsere.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles = "Admin")]
    public class CompetitionsController : Controller {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Private/Competitions
        public ActionResult Index() {
            List<Competition> compets = new List<Competition>();
            try {
                compets = GetCompetitions();
            }
            catch (Exception e) {
                logger.Error(e);
            }

            return View(GetCompetitions());
        }

        // GET: Private/Competitions/Details/5
        public ActionResult Details(int id) {
            List<Competiteur> c = new List<Competiteur>();
            try {
                c = Inscriptions.GetInscriptions(id);
                ViewBag.CompetId = id;


            }
            catch (Exception e) {
                logger.Error(e);
            }

            return View(c);
        }

        /// <summary>
        /// Exporte une compétition vers excel
        /// </summary>
        /// <param name="id">Compétition Id</param>
        /// <returns></returns>
        [HttpGet]
        public FileStreamResult Export(int id) {

            FileStreamResult res = null;

            try {
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
                    writer.Write(c.Categorie.Nom);
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

                res = this.File(output,
                            "application/vnd.ms-excel",
                             compet.Nom + "_" + compet.DateCompetition.ToShortDateString() + ".csv");
            }
            catch (Exception e) {
                logger.Error(e);
            }

            return res;
        }

        [HttpPost]
        public ActionResult Create(Competition compet) {
            List<Competition> compets = new List<Competition>();
            try {
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

                compets = GetCompetitions();
            }
            catch (Exception e) {
                logger.Error(e);
            }

            return View("Index", compets);
        }

        // GET: Private/Competitions/Edit/5
        public ActionResult Edit(int competId) {
            Competition c = new Competition(); ;
            try {
                c = Competition.GetById(competId);

                //Liste des catégories déjà enregistrées
                List<Categorie> listCate = c.Categorie.ToList();
                Session["selectedCategorie"] = listCate;
                List<string> cate = GetCateList(listCate);
                ViewBag.CateList = cate;
            }
            catch (Exception e) {
                logger.Error(e);
            }

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
                logger.Error(e);
                return View();
            }
        }

        public ActionResult Delete(int competId) {
            try {
                Competition c = Competition.GetById(competId);

                if (c != null) {
                    c.Delete();
                }

                return View("Index", GetCompetitions());
            }
            catch (Exception e) {
                logger.Error(e);
                return View();
            }
        }

        /// <summary>
        /// Ajoute une catégorie dans la variable de session qui sera
        /// utilisée pour la création de la compétition
        /// </summary>
        /// <param name="name">Catégorie à ajouter</param>
        public void AddCategorie(string name) {
            try {
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
            catch (Exception e) {
                logger.Error(e);
            }
        }

        /// <summary>
        /// Supprime une catégorie dans la variable de session qui sera
        /// utilisée pour la création de la compétition
        /// </summary>
        /// <param name="name">Catégorie à supprimer</param>
        public void DelCategorie(string name, string competName) {
            Contract.Requires(Session["selectedCategorie"] != null,
                               "La variable de session ne devrait pas être vide à cet endroit du code");
            try {
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
            catch (Exception e) {
                logger.Error(e);
            }
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
                      group c by c.Categorie.Nom into g
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
                      select new { Sexe = g.Key ? "Homme" : "Femme", Count = g.Count() };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Club notification

        /// <summary>
        /// Send an remember email to the clubs which did not subscribe
        /// to the competition
        /// </summary>
        /// <param name="id">Competition identifier</param>
        /// <returns></returns>
        public JsonResult NotifyNonInscrits(int id) {
            /* Attention avec l'hébergement OVH on peut envoyé
             * 200 mails/heure/compte ou 300 mails/heure/IP
             * */
            string msg;

            try {
                List<Club> clubs = Inscriptions.GetNotInscripts(id);
                List<string> parameters = null;
                Competition compet = Competition.GetById(id);
                int remainingDay = (int) (compet.FinInscription - DateTime.Now).TotalDays;

                foreach (Club c in clubs) {
                    if (!string.IsNullOrEmpty(c.Correspondant)) {
                        parameters = new List<string>{
                            c.NomClub,
                            remainingDay.ToString(),
                            compet.Nom
                        };

                        sendEmail(c.Correspondant, "RappelInscriptionCompet", parameters);
                    }
                }

                msg = "Email de rappel envoyé avec succès";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                logger.Error(e);
                msg = "Echec lors de l'envoie du rappel";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        private void sendEmail(string destEmail, string mailIdentifier, List<string> parameters) {

            //Send mail
            using (SmtpClient client = new SmtpClient()) {
                MailMessage mailMessage = new MailMessage();
                ContentType mimeType = new System.Net.Mime.ContentType("text/html");

                //Load the html message linked content
                string filePath = Server.MapPath(Url.Content("~/img/logo.gif"));
                LinkedResource inline = new LinkedResource(filePath, MediaTypeNames.Image.Gif);
                inline.ContentId = "logo";//Guid.NewGuid().ToString();

                parameters.Insert(0, inline.ContentId);
                string subject = string.Empty;
                string message = BuildMsgBody(mailIdentifier, parameters.ToArray(), out subject);


                //Create and the html message to the email
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(message, mimeType);
                avHtml.LinkedResources.Add(inline);
                mailMessage.AlternateViews.Add(avHtml);

                MailAddress fromAdd = new MailAddress("admin@karateisere.fr", "Commission Sportive Karaté Isère");
                mailMessage.From = fromAdd;
                mailMessage.To.Add(destEmail);
                mailMessage.Subject = subject;

                //Add the standard message to the email
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
        }

        private string BuildMsgBody(string mailIdentifier, object[] param, out string subject) {
            string res = "<table>{0}{1}</table>";
            Mail mailHeader = Mail.Get("MailHeader");
            Mail mail = Mail.Get(mailIdentifier);

            subject = mail.Subject;

            res = string.Format(res, mailHeader.Message, mail.Message);
            res = string.Format(res, param);

            return res;
        }

        #endregion
    }
}
