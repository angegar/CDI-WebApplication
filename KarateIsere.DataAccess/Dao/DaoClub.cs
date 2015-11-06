using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoogleAPI.Maps.Services;
using Model = GoogleAPI.Maps.Model;
using GoogleAPI.Maps.Model.Geocoding;

namespace KarateIsere.DataAccess {

    public partial class Club : DaoBase {
        public override sealed void Create() {
            Club c = Get(NumAffiliation);

            if (c == null) {
                if (Art_Martial == null) {
                    Art_Martial = Art_Martial.GetById(1);
                }

                context.Club.Add(this);
                context.SaveChanges();
            }
        }

        public override sealed void Delete() {
            Club c = Get(NumAffiliation);

            if (c != null) {
                context.Club.Attach(c);
                Delete(c, context.Club);
            }
        }

        public override sealed void Update() {
            Contract.Requires(!string.IsNullOrWhiteSpace(NumAffiliation),
                               "Le numero d'affiliation doit etre fournit");

            Club c = Get(NumAffiliation);

            if (c != null) {
                UpdateCoordinate(c);

                Art_Martial = Art_Martial.GetById(art_MartialID, context);
                context.Club.Attach(this);
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private void UpdateCoordinate(Club oldData) {
            string oldAdrr = oldData.Adr_Dojo + " " + oldData.Code_Postal + " " + oldData.Ville;
            string newAdrr = this.Adr_Dojo + " " + this.Code_Postal + " " + this.Ville;

            //Si on n'a pas changé l'adresse on copie les coordonées précédentes
            if (oldAdrr.Equals(newAdrr)) {
                if (oldData.Latitude.HasValue && oldData.Latitude.Value != 0 && this.Latitude == null) {
                    this.Latitude = oldData.Latitude;
                }

                if (oldData.Longitude.HasValue && oldData.Longitude.Value != 0 && this.Longitude == null) {
                    this.Longitude = oldData.Longitude;
                }
            }
            
            //Si on a changé l'adresse ou si les anciennes coordonées sont nules
            //alors on recalcul les coordonnées
            if (!oldAdrr.Equals(newAdrr) ||
                !this.Latitude.HasValue || this.Latitude == 0 ||
                !this.Longitude.HasValue || this.Longitude == 0) {

                //Calculate the new coordinates
                Geocoding geocode = new Geocoding();
                Response resp = geocode.GetByAddress(newAdrr);
                if (resp.Status == "OK") {
                    Results res = resp.Results[0];
                    if (!res.PartialMatch) {
                        this.Latitude = res.Geometry.Location.Latitude;
                        this.Longitude = res.Geometry.Location.Longitude;
                    }
                }
                else {
                    this.Latitude = null;
                    this.Longitude = null;
                }
            }
        }

        public Club ShallowCopy() {
            return (Club) this.MemberwiseClone();
        }

        public static Club GetByNumAffiliation(string numAffiliation) {
            using (KarateIsereContext context = new KarateIsereContext()) {
                Club res = (from c in context.Club
                            where c.NumAffiliation == numAffiliation
                            select c).SingleOrDefault();
                return res;
            }
        }

        public static List<Club> GetAll() {
            List<Club> res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                res = (from c in context.Club
                       select c)
                       .Include(d => d.Art_Martial)
                       .Include(d => d.Adherents)
                       .Include(d => d.Competiteur)
                       .Include(d => d.ListeCompetiteur)
                       .Include(d => d.Notifications)
                       .Include(d => d.Professeurs)
                       .ToList();
            }

            return res;
        }

        public static Club Get(string numAffiliation) {
            Club res = null;
            using (KarateIsereContext context = new KarateIsereContext()) {
                context.Configuration.ProxyCreationEnabled = false;
                res = context.Club.Include(d => d.Competiteur)
                                   .Where(d => d.NumAffiliation == numAffiliation).
                                   SingleOrDefault();
            }

            return res;
        }
    }
}