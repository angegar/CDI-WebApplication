using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;


namespace KarateIsere.Areas.Private.Controllers {
    public class AdminController : Controller {
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            if (ClaimsPrincipal.Current.IsInRole("Admin") || admins == null) {
                ViewBag.Admins = admins != null ? admins : new List<ApplicationUser>();
                return View("CreateAdmin");
            }
            else {
                //redirect to th home page
                return RedirectToRoute("Index", "Home");
            }
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
                string password = string.Empty;
                user = UserManager.FindByEmail(userEmail);

                //Si l'email n'existe pas alors on cré un nouvel utilisateur           
                if (user == null) {
                    password = System.Web.Security.Membership.GeneratePassword(10, 3);

                    user = new ApplicationUser {
                        UserName = userEmail,
                        Email = userEmail
                    };

                    var result = await UserManager.CreateAsync(user, password);

                    if (!result.Succeeded) {
                        logger.Error(result.Errors.First().ToString());
                        throw new Exception(result.Errors.First().ToString());
                    }
                }

                //Affectation des droits administrateurs si l'utilisateur ne les a pas déjà
                Role r = new Role("Admin");

                if (!user.Roles.Any(d => d.RoleId == r.Id)) {
                    r.Grant(user.Id);
                }

                //Envoyer message de confirmation
                if (!string.IsNullOrEmpty(password)) {
                    sendEmail(user.Email, password);
                }
                else {
                    sendEmail(user.Email);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                logger.Error(e);
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

        /// <summary>
        /// Send a confirmation email to the new admin user
        /// </summary>
        /// <param name="toEmail">Admin email</param>
        /// <param name="password">Password</param>
        private void sendEmail(string toEmail, string password = null) {
            //ToDo : c'est assez moche il faut améliorer le mail et
            //peut externaliser la fonction afin de pouvoir la réutiliser

            using (SmtpClient client = new SmtpClient()) {
                try {
                    MailMessage mailMessage = new MailMessage();
                    MailAddress fromAdd = new MailAddress("admin@karateisere.fr", "Karaté Isère");
                    mailMessage.From = fromAdd;
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = "Accès administrateur";
                    mailMessage.Body = "TEST : Bonjour, vous êtes désormais administrateur " +
                                        "de l'application karaté isère.";
                    if (password != null) {
                        mailMessage.Body += "Votre mot de passe est " + password;
                    }

                    client.Send(mailMessage);
                }
                catch (Exception e) {
                    logger.Error(e);
                }

            }

        }
    }
}
