using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {
            List<Formation> listeFormations = new List<Formation>();

            using (var context = new AvisEntities())
            {
                listeFormations = context.Formations.ToList();
            }

            return View(listeFormations);       
        }

        public ActionResult DetailsFormation(String nomSeo)
        {
            var vm = new FormationAvecAvisViewModel();

            using (var context = new AvisEntities())
            {
                var FormationEntity = context.Formations
                    .Where(f => f.NomSeo == nomSeo)
                    .FirstOrDefault();

                if (FormationEntity == null)
                    return RedirectToAction("Acceuil", "Home");

                vm.FormationNom = FormationEntity.Nom;
                vm.FormationDescription = FormationEntity.Description;
                vm.FormationNomSeo = nomSeo;
                vm.FormationUrl = FormationEntity.Url;
                vm.Note = FormationEntity.Avis.Average(a => a.Note);
                vm.NombreAvis = FormationEntity.Avis.Count();
                vm.Avis = FormationEntity.Avis.ToList();
            }
                return View(vm);
        }
    }
}