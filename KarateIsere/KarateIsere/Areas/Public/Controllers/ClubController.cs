﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.DataAccess.Tool;
using KarateIsere.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NLog;

namespace KarateIsere.Areas.Public.Controllers {
    [Authorize(Roles = "User")]
    public class ClubController : Controller {

        private Logger log = LogManager.GetCurrentClassLogger();

        // GET: Public/Club/Edit
        public ActionResult Edit() {
            Contract.Requires(User.Identity.IsAuthenticated,
                              "L'utilisateur doit être authentifié");
            ClubViewModel model = new ClubViewModel();
            
            try {
                IOwinContext ctx = HttpContext.GetOwinContext();
                ApplicationUserManager manager = ctx.GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.Users.First(d => d.Email == User.Identity.Name);
                Club c = Club.Get(user.NumAffiliation);
                Mapper.Map(c, model);
            }
            catch (Exception e) {
                log.Error(e);
            }

            return View("EditClub", model);
        }

        // POST: Public/Club/Edit/5
        [HttpPost]
        public ActionResult Save(ClubViewModel club) {
            Contract.Requires(User.Identity.IsAuthenticated,
                               "L'utilisateur doit être authentifié");
            try {
                if (ModelState.IsValid) {
                    Club c = new Club();
                    Mapper.Map(club, c);
                    c.Update();
                    return View("EditClub", club);
                }
                else {
                    return View("EditClub");
                }
            }
            catch (Exception e) {
                log.Error(e);
                return View("EditClub");
            }
        }

        // GET: Public/Club/Delete/5
        public ActionResult Delete(int id) {
            return View();

        }

        // POST: Public/Club/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, System.Web.Mvc.FormCollection collection) {
            try {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }
    }
}
