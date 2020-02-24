using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class SwController : Controller
    {
        [HttpGet]
        public JsonResult AddProduit(int idProduit, string idSession)
        {
            List<int> panier = new List<int>();
            panier.Add(idProduit);
            HttpContext.Application[idSession] = "";
            return Json("toto", JsonRequestBehavior.AllowGet);
        }
    }
}