using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace KarateIsere.Areas.Private.Controllers {
    public class AdminController : Controller {
        private ApplicationUserManager _userManager;
        private ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set {
                _userManager = value;
            }
        }

        // GET: Private/Admin
        public ActionResult Index() {
            return View("CreateAdmin");
        }

        // GET: Private/Admin/Details/5
        public ActionResult Details(int id) {
            return View();
        }

        // POST: Private/Admin/Create
        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection) {
            try {
                string userEmail = collection["email"].ToString();
                string password = System.Web.Security.Membership.GeneratePassword(10, 3);

                //Vérifier que le compteur utilisateur n'existe pas déjà sinon
                //il faut l'utiliser et lui ajouter les droits nécesaires

                var user = new ApplicationUser {
                    UserName = userEmail,
                    Email = userEmail            
                };

                var result = await UserManager.CreateAsync(user, password);

                if (result.Succeeded) {
                    Role r = new Role("Admin");
                    r.Grant(user.Id);

                    //To Do : envoyer une email de confirmation avec le mot de passe
                }
                else {
                    throw new Exception("L'administrateur n'a pas été créé");
                }

                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // GET: Private/Admin/Edit/5
        public ActionResult Edit(int id) {
            return View();
        }

        // POST: Private/Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // GET: Private/Admin/Delete/5
        public ActionResult Delete(int id) {
            return View();
        }

        // POST: Private/Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        /// <summary>
        /// Generate a new password
        /// </summary>
        /// <returns>A new password as a string</returns>
        private string generatePassword() {

            throw new NotImplementedException();
        }
    }
}
