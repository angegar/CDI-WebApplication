using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using NLog;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles = "User")]
    public class CompetiteursController : Controller {
        private ApplicationUserManager _userManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        // GET: Private/Competiteurs
        public ActionResult Index() {
            if (Session["User"] != null) {
                 Init();
                return View();
            }
            else {
                return RedirectToAction("login", "Account", new {
                    area = "",
                    ReturnUrl = HttpContext.Request.Url.ToString()
                });
            }
        }

        private ApplicationUser Init() {
            ApplicationUser appUser = (ApplicationUser) Session["User"];
            Club c = appUser.Club;
            c = Club.Get(c.NumAffiliation);
            ViewBag.Categories = GetCategorieList();
            ViewBag.Competiteurs = c.Competiteur.ToList();

            return appUser;
        }

        private List<SelectListItem> GetCategorieList() {
            List<Categorie> cList = Categorie.GetAll();
            List<SelectListItem> res = new List<SelectListItem>();

            foreach (Categorie c in cList) {
                SelectListItem sli = new SelectListItem();
                sli.Text = c.Nom;
                sli.Value = c.Nom;
                res.Add(sli);
            }

            return res;
        }

        // POST: Private/Competiteurs/Create
        [HttpPost]
        public ActionResult Create(Competiteur competiteur) {
            try {
                if (ModelState.IsValid) {
                    ApplicationUser user = (ApplicationUser) Session["User"];
                    competiteur.NumAffiliation = user.NumAffiliation;
                    competiteur.Create();
                    return RedirectToAction("Index");
                }
                else {
                    Init();
                    return View("Index", competiteur);
                }
            }
            catch (Exception e) {
                logger.Error(e);
                return View("Index");
            }
        }

        // GET: Private/Competiteurs/Edit/5
        public ActionResult Edit(string id) {
            Competiteur c = Competiteur.Get(id);
            ViewBag.Categories = GetCategorieList();
            return View(c);
        }

        // POST: Private/Competiteurs/Edit/5
        [HttpPost]
        public ActionResult Edit(Competiteur c) {
            try {
                c.Update();
                //ViewBag.Categories = GetCategorieList();
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                logger.Error(e);
                return View();
            }
        }

        // GET: Private/Competiteurs/Delete/5
        public ActionResult Delete(string id) {
            try {
                Competiteur c = new Competiteur();
                c.NumLicence = id;
                // Delete cascade on a compétiteur delete
                c.Delete();
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                logger.Error(e);
                return View();
            }
        }
    }
}
