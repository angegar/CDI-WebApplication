using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoogleAPI.Maps.Services;
using Model = GoogleAPI.Maps.Model;
using GoogleAPI.Maps.Model.Geocoding;
using KarateIsere.DataAccess;
using Newtonsoft.Json;
using NLog;

namespace KarateIsere.Areas.Public.Controllers {
    public class ClubMapController : Controller {
        Logger log = LogManager.GetCurrentClassLogger();

        // GET: Public/ClubMap
        public ActionResult Carte() {
            try {
                List<Club> clubs = Club.GetAll();
                UpdateCoordonne(clubs);
                string json = JsonConvert.SerializeObject(clubs);
                ViewBag.ClubInfos = json;
            }
            catch (Exception e) {
                log.Error(e);
            }

            return View("Carte");
        }

        public ActionResult Index() {
            List<Club> clubs = new List<Club>();
            try {
                clubs = Club.GetAll();
            }
            catch (Exception e) {
                log.Error(e);
            }
            return View(clubs);
        }

        private void UpdateCoordonne(List<Club> clubs) {
            Geocoding geocode = new Geocoding();

            foreach (Club c in clubs) {
                if (!c.Latitude.HasValue || c.Latitude == 0 ||
                    !c.Longitude.HasValue || c.Longitude == 0) {

                    if (!string.IsNullOrEmpty(c.Ville) &&
                        !string.IsNullOrEmpty(c.Adr_Dojo)) {

                        string addr = c.Adr_Dojo + " " + c.Ville;

                        if (!string.IsNullOrEmpty(c.Code_Postal)) {
                            addr = c.Adr_Dojo + " " + c.Code_Postal + " " + c.Ville;
                        }

                        Model.Geocoding.Response resp = geocode.GetByAddress(addr);

                        if (resp.Status == "OK") {
                            Results res = resp.Results[0];
                            if (!res.PartialMatch) {
                                c.Latitude = res.Geometry.Location.Latitude;
                                c.Longitude = res.Geometry.Location.Longitude;
                                c.Update();
                            }
                        }
                    }
                }
            }
        }
    }
}
