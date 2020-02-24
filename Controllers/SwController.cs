using AfpEat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class SwController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();

        [HttpPost]
        public JsonResult AddProduit(int idProduit = 0, string idSession = "")
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            if (sessionUtilisateur != null)
            {

                List<int> panier = new List<int>();
                panier.Add(idProduit);
                HttpContext.Application[idSession] = panier;

            }
            return Json("toto", JsonRequestBehavior.AllowGet);

        }
    }
}