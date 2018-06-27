using AvisFormation.Data;
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
            using (var context = new AvisEntities())
            {
                var listeFormations = context.Formations.ToList();
            }

            return View();
        }
    }
}