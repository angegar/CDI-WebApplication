using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;

namespace KarateIsere.Areas.Private.Controllers {
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller {
        // GET: Private/Categories
        public ActionResult Index () {
            InitCategorie();
            return View();
        }

        // GET: Private/Categories/Details/5
        public ActionResult Details ( int id ) {
            return View();
        }

        [HttpPost]
        public ActionResult Create ( Categorie cate ) {          
            if (ModelState.IsValid) {
                cate.Create();
                return RedirectToAction("Index");
            }

            InitCategorie();
            return View("Index",cate);
        }

        public ActionResult Delete ( string nomCate ) {
            Categorie c = new Categorie {
                Nom = nomCate
            };

            c.Delete();
            return RedirectToAction( "Index" );
        }

        private void InitCategorie() {
            List<Categorie> cates = Categorie.GetAll();
            ViewBag.Categories = cates;
        }
    }
}
