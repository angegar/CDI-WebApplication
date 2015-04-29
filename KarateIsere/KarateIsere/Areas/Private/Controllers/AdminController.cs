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
            //Get list of admin account
            Role r = new Role("Admin");
            List<ApplicationUser> admins = r.GetUsers();
            ViewBag.Admins = admins != null ? admins : new List<ApplicationUser>();

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
                ApplicationUser user = null;
                user = UserManager.FindByEmail(userEmail);

                if (user == null) {
                    //Créer un nouvel utilisateur et luid onner les droits admin

                    //ToDO: Vérifier la politique de sécurité pour la génération du mot de passe
                    string password = System.Web.Security.Membership.GeneratePassword(10, 3);

                    user = new ApplicationUser {
                        UserName = userEmail,
                        Email = userEmail
                    };

                    var result = await UserManager.CreateAsync(user, password);

                    if (!result.Succeeded) {
                        throw new Exception(result.Errors.First().ToString());
                    }
                    else {
                        //ToDo : envoyer une email de confirmation avec le mot de passe
                    }
                }
                else {
                    Role r = new Role("Admin");
                    //Si l'utilisateur n'a pas le role alors grant
                    if (!user.Roles.Any(d => d.RoleId == r.Id)) {
                        r.Grant(user.Id);
                    }
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

        // POST: Private/Admin/Delete/5
        // [HttpPost]
        public ActionResult Delete(string id) {
            try {
                // TODO: Add delete logic here
                Role r = new Role("Admin");
                r.UnGrant(id);
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
