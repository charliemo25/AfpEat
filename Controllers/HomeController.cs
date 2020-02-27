using AfpEat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class HomeController : Controller
    {

        private AfpEatEntities db = new AfpEatEntities();

        public ActionResult Index()
        {
            
            ViewBag.Restaurants = db.Restaurants.ToList();
            return View(db.TypeCuisines.ToList());
        }

        public ActionResult Panier()
        {
            PanierViewModel panier = (PanierViewModel)HttpContext.Application[Session.SessionID];
            List<PanierViewModel> paniers = new List<PanierViewModel>();
            paniers.Add(panier);
            List<MenuPanier> menuPaniers = panier.menuPaniers;
            List<ProduitPanier> produitPaniers = panier.produitPaniers;

            ViewBag.menuPaniers = menuPaniers;

            return View(paniers.ToList());
        }

        public ActionResult PanierAjax()
        {
            return View();
        }

    }
}