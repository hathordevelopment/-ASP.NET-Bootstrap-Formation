using AvisFormation.Data;
using AvisFormation.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvisFormation.WebUi.Controllers
{
    public class AvisController : Controller
    {
        // GET: Avis
        public ActionResult LaisserUnAvis(string nomSeo)
        {
            var vm = new LaisserUnAvisViewModel();
            vm.NomSeo = nomSeo;
            vm.FormationName = "";
            using (var context = new AvisEntities())
            {
                var formationEntity = context.Formations
                    .FirstOrDefault(f => f.NomSeo == nomSeo);

                if (formationEntity == null)
                    return RedirectToAction("Acceuil", "Home");

                vm.FormationName = formationEntity.Nom;
            }

            return View(vm);
        }

        //  public ActionResult SaveComment(string commentaire, string nom, string note, string nomSeo)
        public ActionResult SaveComment(SaveCommentViewModel comment)
        {
            Avis nouvelAvis = new Avis
            {
                DateAvis = DateTime.Now,
                Description = comment.Commentaire,
                Nom = comment.Nom
            };

            double dNote = 0;
            if(!double.TryParse(comment.Note, NumberStyles.Any, CultureInfo.InvariantCulture, out dNote))
            {
                throw new Exception("Impossible de parser la note " + comment.Note);
            }
            nouvelAvis.Note = dNote;

            using (var context = new AvisEntities())
            {
                var formationEntity = context.Formations
                    .FirstOrDefault(f=>f.NomSeo == comment.NomSeo);

                if (formationEntity == null)
                    return RedirectToAction("Acceuil", "Home");

                nouvelAvis.IdFormation = formationEntity.Id;

                context.Avis.Add(nouvelAvis);
                context.SaveChanges();
            }

            return RedirectToAction("DetailsFormation", "Formation", new { nomSeo = comment.NomSeo });
        }
    }
}