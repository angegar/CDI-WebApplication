﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;


namespace KarateIsere.Areas.Private.Controllers {
    public class RoleController : Controller {
        // GET: Private/Role
        public ActionResult Index() {
            Role r = new Role();
            List<string> roles = r.Get().Select(d => d.Name).ToList();
            ViewBag.Roles = roles;
            return View();
        }

        // POST: Private/Role/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                // TODO: Add insert logic here
                string roleName = collection["roleName"].ToString();
                Role r = new Role(roleName);
                r.Create();
                return RedirectToAction("Index");
            }
            catch {
                return View();
            }
        }

        // GET: Private/Role/Delete/5
        public ActionResult Delete(string id) {
            Role r = new Role(id);
            r.Delete();
            return RedirectToAction("Index");
        }
    }
}
