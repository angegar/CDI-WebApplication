using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using NLog;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller {
        Logger log = LogManager.GetCurrentClassLogger();

        // GET: Private/Categories
        public ActionResult Index () {
            try {
                InitCategorie();
            }
            catch (Exception e) {
                log.Error(e);
            }
            return View();
        }

        // GET: Private/Categories/Details/5
        public ActionResult Details ( int id ) {
            return View();
        }

        [HttpPost]
        public ActionResult Create ( Categorie cate ) {
            try {
                if (ModelState.IsValid) {
                    cate.Create();
                    return RedirectToAction("Index");
                }

                InitCategorie();
            }
            catch (Exception e) {
                log.Error(e);
            }

            return View("Index",cate);
        }

        public ActionResult Delete ( string nomCate ) {
            try {
                Categorie c = new Categorie {
                    Nom = nomCate
                };

                c.Delete();              
            }
            catch (Exception e) {
                log.Error(e);
            }

            return RedirectToAction("Index");
        }

        private void InitCategorie() {
            List<Categorie> cates = Categorie.GetAll();
            ViewBag.Categories = cates;
        }
    }
}
