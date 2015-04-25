using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarateIsere.DataAccess;
using KarateIsere.DataAccess.Tool;
using Model=KarateIsere.DataAccess;

namespace KarateIsere.Models {
    public class CompetitionViewModel : Model.Competition {
        public IEnumerable<SelectListItem> CategorieList {
            get {
                //Enlève les catégories déjà sélectionnées de la liste des catégories
                List<String> cateList = Model.Categorie.GetAll().Select( d => d.Nom ).ToList();
                cateList.RemoveAll( d => SelectedCategorie.Contains( d ) );

                return cateList.Select( d =>
                                new SelectListItem {
                                    Value = d,
                                    Text = d
                                } );

            }
        }

        private List<string> selectedCategorie;

        public List<string> SelectedCategorie {
            get {
                if (Categorie != null && Categorie.Count > 0) {
                    selectedCategorie =  Categorie.Select( d => d.Nom ).ToList();
                }

                if (selectedCategorie == null) {
                    selectedCategorie = new List<string>();
                }

                return selectedCategorie;
            }
        }

        public override void Create () {
            //on a besoins de faire cette manipulation car CompetitionViewModel hérite
            //de Competition mais ne possède pas d'information de mappage pour Entity Framework
            Competition c = this;
            (c as Competition).Create();
        }

        public override void Update () {
            //on a besoins de faire cette manipulation car CompetitionViewModel hérite
            //de Competition mais ne possède pas d'information de mappage pour Entity Framework
            Competition c = (Competition) this;
            c.Update();
        }

        /// <summary>
        /// Methode utilisée pour mapper le type proxy compétition sur l'objet compétition view model
        /// </summary>
        /// <param name="competitions"></param>
        /// <returns></returns>
        public static List<CompetitionViewModel> ToCompetitionViewModel ( List<Competition> competitions ) {
            List<CompetitionViewModel> res = new List<CompetitionViewModel>();

            foreach (Competition c in competitions) {
                CompetitionViewModel obj = new CompetitionViewModel();
                Mapper.Map( c, obj );
                res.Add( obj );
            }

            return res;
        }

    }
}