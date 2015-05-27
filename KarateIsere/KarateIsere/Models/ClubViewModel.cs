using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;

namespace KarateIsere.Models {
    public class ClubViewModel : Club {

        public IEnumerable<SelectListItem> ArtMartiaux {
            get {
                return Art_Martial.GetAll().Select( d =>
                                new SelectListItem {
                                    Value = d.Name,
                                    Text = d.Name
                                } );
            }
        }
    }
}